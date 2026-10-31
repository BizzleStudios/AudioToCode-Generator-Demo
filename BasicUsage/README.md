# Audio to Code Generator (Indie Edition) v1.1

![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-blue)
![Version](https://img.shields.io/badge/Version-1.1.0-orange)
![Platform](https://img.shields.io/badge/Platform-Editor-lightgrey)

**Convert audio clips directly into C# bytecode arrays for runtime procedural audio without external audio dependencies.**

---

## Overview

**Audio to Code Generator (Indie Edition)** transforms standard Unity AudioClips into pure, executable C# code. This eliminates the need for loose audio files (`.wav`, `.mp3`, `.ogg`) in your builds, ensuring zero external asset loading overhead.

### Key Use Cases
- **Platformers & Action Games**: Instant playback for jumps, coins, hits, and power-up SFX.
- **Retro & Pixel Art Titles**: Lightweight 8-bit sound synthesis directly generated from code.
- **Procedural Sound Systems**: Pure programmatic audio without file I/O bottlenecks.
- **Ultra-Lightweight Builds**: Drastically reduce asset streaming packages and project size.

---

## Features

- **Instant Conversion**: One-click transformation of AudioClips into embedded C# bytecode arrays.
- **4 Export Formats**:
  1. **Gzip + Base64**: Highly compressed string bytecode for balanced file size and memory footprint.
  2. **Lazy AudioClip Provider**: Ultra-lean static class with constant duration metadata and deterministic memory cleanup (`Release()` / `Unload()`).
  3. **Raw PCM 16-Bit Array**: Direct sample array without decompression overhead, optimized for Micro-SFX (0.1s – 2.0s).
  4. **Standalone MonoBehaviour**: Ready-to-use component with built-in trigger settings and inspector playback controls.
- **Multi-Slot Workflow**: Manage up to 10 independent sound slots with dedicated preview and export configurations.
- **Studio-Grade Audio DSP**:
  - Real-time Volume Gain adjustment.
  - Turbo-WSOLA playback speed control (tempo change without pitch shifting).
  - Decoupled pitch shifting (musical transposition without altering playback duration).
  - Analog Tape Mode (synchronized speed & pitch scaling).
  - Peak 0.0 dBFS auto-normalization with transparent soft limiting.
  - Balanced Mono / Stereo downmixing (`(L + R) * 0.5f`).
- **Smart Overwrite Export**: Safely updates existing code files in the same export directory without breaking game script references or bloating the project.
- **Waveform Visualizer**: Interactive zoomable timeline with musical note badge (`♫ Note [cents]`) and dynamic peak dBFS clipping indicators.

---

## Getting Started

### Step 1: Open the Generator
Navigate to the top menu in Unity:
> **Tools > Audio to Code Generator (Indie Edition) > 🎛️ Audio to Code Generator v1.1**

### Step 2: Assign Master Audio
Drag and drop any `AudioClip` into the **Master Audio** field or one of the **Audio Slots**.

### Step 3: Tune & Trim
Adjust playback speed, pitch, volume gain, or trim markers in the interactive waveform timeline.

### Step 4: Export Code
- Press **Ctrl+Shift+C** to copy the generated C# bytecode to your clipboard, or
- Press **Ctrl+S** to export ready-to-use `.cs` scripts directly into your project's `Assets/Audio to Code Exports` directory.

---

## Example Runtime Usage

Using the generated static audio provider in your game scripts:

```csharp
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        // One-line playback using the generated helper
        JumpSoundAudio_Gzip.Play(volume: 1.0f, pitch: 1.0f);
    }
}