# Changelog

All notable changes to Volume Protector are documented here.  
Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [Unreleased]
- Per-application (session-level) volume limits via `IAudioSessionControl2`
- Configurable Night Mode time window in Settings UI
- Minimum volume floor (ensure notification sounds always audible)
- System tray icon changes colour based on protection state

---

## [2.0.0] — 2026-02-27

### ⚡ Changed — Core Architecture
- **Replaced polling timer with `IAudioEndpointVolumeCallback`** — enforcement is now instant.  
  Previous design checked volume every 100ms, meaning a spike could play for up to 100ms before being caught.  
  The callback fires synchronously when Windows Core Audio processes a volume change request — before audio is sent to the output buffer. Zero window.
- VolumeGuard now tags its own `SetMasterVolumeLevelScalar` calls with a sentinel GUID (`OurEventGuid`) to avoid triggering its own callback (infinite loop prevention).
- Renamed all single-letter aliases and obfuscated class names to proper descriptive names for maintainability.
- Split monolithic class structure into logical regions: COM Interfaces, AudioUtil, VolumeGuard, Controls, Config, Settings, Program.

### Added
- **Night Mode** — tighter automatic cap (default 40%) between 10pm and 7am. Toggleable from tray menu or Settings. Applies only if tighter than the per-device limit.
- **PIN Lock** — optional 4-digit PIN on the Settings dialog. Prevents accidental or intentional bypassing of limits.
- **Quick Presets** — tray right-click submenu: Safe (40%), Normal (70%), High (90%). Applies to all devices instantly.
- **Clamp notifications** — balloon toast when a volume spike is caught, showing old % → clamped %. 12-second cooldown per device to avoid spam.
- **Colour-coded sliders** — green ≤60% (safe), amber ≤80% (moderate), red >80% (loud). Visual risk feedback while adjusting.
- **Live limit label** — slider label updates in real time while dragging (was: only on release).
- **Immediate enforcement on settings save** — when a slider is moved, limits are enforced right away via `RefreshGuards()`, not on the next callback.
- **Legacy config support** — reads old format (`DeviceName=value`) as well as new (`limit.DeviceName=value`).
- Config now persists Night Mode settings, PIN, and device limits with named keys.

### Fixed
- Closure capture bug in device loop (was capturing loop variable, now captures local copy).
- Tray tooltip max length capped at 63 chars (Windows limit) to prevent silent truncation.

---

## [1.1.0] — 2026-02-20

### Added
- Dark-themed custom context menu renderer (`DarkMenuRenderer` + `DarkColorTable`)
- "Launch on Start" toggle writes directly to `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`
- Per-device mute toggle synced bidirectionally (external mute changes reflected in Settings UI within 500ms)
- Settings form is now draggable (no title bar needed)
- Custom teal border paint on Settings form

### Changed
- Settings window minimum size increased to 600×200 to accommodate wider device names
- Timer interval for mute sync reduced from 1000ms to 500ms

### Fixed
- Settings form could be opened multiple times simultaneously — now uses `ShowDialog()` to enforce modal behaviour

---

## [1.0.0] — 2026-02-13

### First public release

- System tray application, single `.exe`, no installer
- Per-device maximum volume enforcement via 100ms polling timer
- Custom `VolumeSlider` control (teal fill, white thumb)
- Custom `ToggleSwitch` control (pill shape, animated)
- Borderless dark-theme Settings window
- Config persisted to `%AppData%\VolumeProtectorSettings.txt`
- Single-instance mutex (`VolumeProtectorMutex`)
- `IAudioEndpointVolume`, `IMMDevice`, `IMMDeviceEnumerator` COM wrappers (P/Invoke, no external DLLs)
- `IPropertyStore` used to retrieve friendly device name (`PKEY_Device_FriendlyName`)
