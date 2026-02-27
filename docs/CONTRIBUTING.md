# Contributing

Thanks for your interest in improving Volume Protector!  
Because this is a hearing-safety tool, quality and correctness are prioritised over feature velocity.

---

## Before You Open a PR

1. **Check the [Roadmap](ROADMAP.md)** — your idea may already be planned or intentionally deferred.
2. **Open an issue first** for anything larger than a typo fix, so we can discuss the design before you invest time coding.
3. **No dependencies** — the project must remain a single `.cs` file compilable with the inbox `csc.exe`. No NuGet packages, no external DLLs.

---

## Setup

No IDE required. All you need is PowerShell:

```powershell
# Clone
git clone https://github.com/Santaslileper/Ear-protection.git
cd Ear-protection

# Build
& "C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" /target:winexe /win32icon:../assets/icon.ico /out:"VolumeProtector.exe" "../src/VolumeProtector.cs"

# Test — run it and check the tray icon appears
Start-Process ".\VolumeProtector.exe"

# Kill before rebuilding
Stop-Process -Name "VolumeProtector" -Force -ErrorAction SilentlyContinue
```

---

## Code Style

- **No single-letter names** (except loop counters). The project was refactored away from obfuscated naming in v2.0.0 — please keep it readable.
- **Regions** for major sections (`COM Interfaces`, `Audio Helpers`, `Controls`, etc.)
- **Error handling**: bare `catch {}` is acceptable in COM callback paths only — everywhere else, fail visibly or log to `VolumeProtectorLog.txt`.
- **Thread safety**: `OnNotify` is called from a COM apartment thread. Any UI operations (tray tooltips, balloon tips) must be safe to call from background threads. `NotifyIcon.ShowBalloonTip` is safe; `Form.Invoke` is required for controls.

---

## COM Interface Changes

The COM vtable orderings in the interface declarations must exactly match the Windows SDK headers. If you need to add methods:

1. Reference the official SDK header (e.g. `mmdeviceapi.h`, `endpointvolume.h`) or the MSDN docs.
2. Add placeholder `int _N()` stubs for any methods that fall before the ones you need — do not skip vtable slots.
3. Test on at least two different audio device types (USB + onboard).

---

## What Makes a Good Bug Report

- Windows version (`winver`)
- Audio device name (as shown in Windows Sound settings)
- Audio driver version (Device Manager → Properties → Driver)
- Steps to reproduce
- Expected behaviour vs. actual behaviour
- Whether the issue occurs with protection disabled (helps narrow down COM vs. UI issues)

---

## What Makes a Good Feature Request

- The problem it solves (not just "it would be cool")
- Whether it can be implemented without adding external dependencies
- Any relevant Windows API surface you've identified

---

## Sensitive Areas

These areas require extra care because a bug could fail to protect the user:

| Area | Risk |
|------|------|
| `VolumeGuard.OnNotify` | Must never throw unhandled — COM callback failures can crash the audio service |
| `OurEventGuid` check | Must be correct or we get infinite callback recursion |
| `GetEffectiveLimit` | Returning 1.0f (100%) when limits exist = protection bypass |
| `RefreshGuards` | Must unregister all old guards before registering new ones to avoid duplicates |
