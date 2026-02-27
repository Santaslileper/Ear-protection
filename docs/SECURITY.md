# Security Policy & Safety Verification

## 🛡️ False Positive Notice: `Win32/Bearfoos.B!ml`

Because Volume Protector is a lightweight, unsigned utility that interacts with Windows Audio Services and is compiled using the built-in `csc.exe` tool, Microsoft Defender may flag it with the machine-learning label **`Win32/Bearfoos.B!ml`**.

### Why this happens:
*   **Behavioral Heuristics**: The app "hooks" into the Windows Audio Engine to enforce volume limits. To an AI, "hooking" looks like suspicious activity.
*   **No Digital Signature**: We don't have a $300/year code-signing certificate, so Windows treats the file as "Unknown."
*   **Legacy Compiler**: Using the built-in .NET 4.0 compiler (`csc.exe`) is a technique often used by malware to build itself on-the-fly, so antivirus AI is extra paranoid about it.

---

## 🔬 Deep Behavioral Analysis (Sandbox Results)

We transparency-check every build. Here is what an automated sandbox (like VirusTotal or JoeSandbox) sees when it runs Volume Protector, and why it's safe:

### ✅ Network & Communication
- **Network Comms: NOT FOUND**
- **IDS/Sigma Rules: NOT FOUND**
- **Dropped Files: NOT FOUND**
- **Result:** The app is 100% offline. It cannot "exfiltrate" data because it has no networking code.

### 🔍 System Interaction (MITRE ATT&CK)
| Tactic | Technique | Explanation |
| :--- | :--- | :--- |
| **Persistence** | T1547.001 | **Registry Run Key:** This is triggered if you enable "Launch on Start". It writes to `HKCU\...\Run` so the app starts with Windows. |
| **Defense Evasion** | T1112 | **Modify Registry:** The app saves your volume limits to the Windows Registry if you use the Startup toggle. |
| **Discovery** | T1083 / T1012 | **File/Registry Discovery:** The app looks for its own settings file (`VolumeProtectorSettings.txt`) and queries Windows for your audio device names. |

### 🛠️ Key File & Registry Actions
- **Mutex Created:** `VPM` (Ensures only one instance of the protector is running).
- **Files Accessed:** `AppData\Roaming\VolumeProtectorSettings.txt` (This is where your limits and Night Mode settings are stored).
- **COM Interfaces:** Accesses `{BCDE0395-E52F-467C-8E3D-C4579291692E}` (The standard Windows Multimedia Device Enumerator).

---

## 🔍 How to Verify the Code is Safe

The best thing about this project is that it is **100% Transparent**. You don't have to trust the `.exe`.

### 1. Read the Source
Check [VolumeProtector.cs](../src/VolumeProtector.cs). You will see that it:
*   Does **NOT** have any network/internet code.
*   Does **NOT** read your personal files or browser data.
*   **ONLY** communicates with `IMMDevice` and `IAudioEndpointVolume` (standard Windows audio APIs).

### 2. Compile it Yourself
Instead of running our pre-built `.exe`, you can build it from the source code in 5 seconds. Windows treats a binary you built yourself more favorably than one downloaded from the internet.

```powershell
# Open PowerShell in the project folder and run:
& "C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" /target:winexe /out:"MyVolumeProtector.exe" "../src/VolumeProtector.cs"
```

### 3. VirusTotal Check
You can upload the `.exe` to [VirusTotal.com](https://www.virustotal.com). You will likely see that 60+ reputable engines (Malwarebytes, Kaspersky, Bitdefender) find it clean, while only 1 or 2 (Microsoft) flag it due to the machine-learning AI.

---

## 🛡️ Reporting a Real Security Vulnerability

If you've discovered a real security vulnerability in this project (e.g., a way a malicious app could crash Volume Protector to cause a spike), please open an issue or contact the maintainer directly.

**Protect your ears. Stay safe.**
