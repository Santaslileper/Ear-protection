# Code Map — Variable & Class Translation Key

The source code uses short aliases for compactness and minimal binary size.
This document provides a full translation so any developer can read the code.

---

## Namespace Aliases (`using` statements)

| Alias | Full Namespace |
|-------|---------------|
| `S`   | `System` |
| `R`   | `System.Runtime.InteropServices` |
| `M`   | `System.Threading.Mutex` |
| `I`   | `System.IO.File` |
| `Pth` | `System.IO.Path` |
| `C`   | `System.Collections.Generic.Dictionary<string, float>` |
| `F`   | `System.Windows.Forms` |
| `D`   | `System.Drawing` |
| `G`   | `System.Drawing.Drawing2D` |
| `W`   | `Microsoft.Win32.Registry` |
| `E`   | `System.Environment` |

---

## COM Interface & Struct Names

| Short | Full Name | Description |
|-------|-----------|-------------|
| `PS`  | `IPropertyStore` | COM interface — reads device property bag |
| `PK`  | `PropertyKey` | Struct — key to look up a property (GUID + ID) |
| `PV`  | `PropVariant` | Struct — holds a property value (e.g. device name string) |
| `EV`  | `IAudioEndpointVolume` | COM interface — gets/sets volume on an audio endpoint |
| `MD`  | `IMMDevice` | COM interface — represents a single audio device |
| `MC`  | `IMMDeviceCollection` | COM interface — a list of audio devices |
| `ME`  | `IMMDeviceEnumerator` | COM interface — enumerates all audio devices |
| `MM`  | `MMDeviceEnumeratorComObject` | COM class — the concrete object that implements `ME` |

---

## Utility Class `U` (Audio Helpers)

| Call | Full Name | Description |
|------|-----------|-------------|
| `U.V(d)` | `GetVolumeInterface(device)` | Returns the `IAudioEndpointVolume` for a device |
| `U.N(d)` | `GetFriendlyName(device)` | Returns the human-readable device name |
| `U.A()` | `GetActiveRenderEndpoints()` | Yields all active audio output devices |

---

## UI Classes

| Short | Full Name | Description |
|-------|-----------|-------------|
| `SS`  | `ModernSlider` | Custom-drawn horizontal volume limit slider |
| `ST`  | `ModernToggle` | Custom-drawn pill-shaped toggle switch |
| `SF`  | `SettingsForm` | The settings window (no title bar, draggable) |
| `DR`  | `DarkMenuRenderer` | Dark theme renderer for the tray context menu |
| `DC`  | `DarkColorTable` | Dark color palette used by `DR` |

---

## Program / App Core (`Prg` class)

| Short | Full Name | Description |
|-------|-----------|-------------|
| `Prg.L` | `DeviceLimits` | `Dictionary<string, float>` — device name → volume cap (0–100) |
| `Prg.Ft` | `ConfigFilePath` | Full path to `VolumeProtectorSettings.txt` in `%AppData%` |
| `Prg.WriteCfg()` | `SaveSettings()` | Writes limits to disk |
| `Prg.ReadCfg()` | `LoadSettings()` | Loads limits from disk on startup |
| `Prg.IsStart()` | `IsAutoStartEnabled()` | Checks the Windows Run registry key |
| `Prg.SetStart(e)` | `SetAutoStart(enabled)` | Adds/removes the app from Windows startup |

---

## Key Constants & Literals

| Value | Meaning |
|-------|---------|
| `"VPM"` | Mutex name — prevents more than one instance running |
| `"VP"` | Registry value name for Windows auto-start |
| `Interval=100` | Protection timer fires every 100ms |
| `Interval=500` | UI sync timer fires every 500ms |
| `0.005f` | Volume tolerance — only clamps if `current > limit + 0.5%` |
| `Guid "5CDF2C82..."` | IID of `IAudioEndpointVolume` COM interface |
| `Guid "A95664D2..."` | IID of `IMMDeviceEnumerator` COM interface |
| `Guid "BCDE0395..."` | CLSID of `MMDeviceEnumerator` COM class |
| `pid = 14` | Property ID for device friendly name (`PKEY_Device_FriendlyName`) |
