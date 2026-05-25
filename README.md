# STM32 to Unity: Digital Twin Telemetry Bridge

A lightweight Hardware-in-the-Loop (HIL) prototype demonstrating real-time serial communication between an STM32 microcontroller and the Unity Engine. 

This project bridges the gap between embedded test automation and 3D industrial simulation, proving out the data pipeline required for rendering physical hardware states in a digital environment.

## 🛠️ Tech Stack
* **Hardware:** NUCLEO-G491RE (ARM Cortex-M4), Grove EMG Detector
* **Firmware:** Embedded C via Standalone STM32CubeMX & STM32CubeIDE (HAL Drivers)
* **Engine:** Unity 2022.3 LTS (Built-In Render Pipeline)
* **Scripting:** C# (.NET Framework)
* **Environment:** Ubuntu Linux (`/dev/ttyACM0` Serial Architecture)

---

## 🧬 Phase 2: Biometric Digital Twin (Active)
Transitioning from synthetic data to physical human-machine interaction, this phase captures raw electromyography (EMG) signals and translates them into a scalable 3D digital twin.

> **Hardware Setup:** *(Placeholder for Hardware Setup*

> **Live Demo:** *(Placeholder for Demo Record*

### How It Works:
1. **Biometric Capture:** A Grove EMG Detector is attached to the user's forearm (targeting the Flexor Carpi group). 
2. **ADC Conversion:** The STM32 reads the analog voltage (1.5V standby to 3.3V active flex) using a 12-bit ADC, mapping the electrical muscle potential to a digital range of `~1861` to `4095`.
3. **Data Pipeline:** The raw integers are formatted with carriage returns (`\r\n`) and blasted over the `LPUART1` virtual COM port at 115200 baud.
4. **Unity Integration:** The C# `EMGScaleController.cs` script captures the serial stream, normalizes the data against the baseline, and utilizes Linear Interpolation (`Vector3.Lerp`) to smoothly inflate and deflate a 3D sphere in real-time based on actual muscle tension.

---

## ⚙️ Phase 1: Synthetic Telemetry Prototype (Archived)
The foundational experiment to establish the serial bridge using a purely synthetic data stream.

https://github.com/user-attachments/assets/40937783-edf9-4bc1-b2c9-1efa2059e50f

### How It Works:
1. **The Synthetic Sensor:** The STM32 generates a continuous, sweeping synthetic telemetry signal (0-358 degrees) to simulate a physical rotary encoder.
2. **The C# Serial Bridge:** A highly optimized Unity script (`DialController.cs`) captures the live COM port data, parses the string, and applies the absolute angle directly to a 3D cylinder's transform matrix in real-time.

---

## 🚀 Next Steps
* [x] Integrate a physical sensor (e.g., potentiometer, IMU, or depth vision camera).
* [ ] Expand the telemetry packet to include multi-axis data and system diagnostics.
* [ ] Apply this biometric data pipeline to physical exoskeleton control loops or robotic rehabilitation prototypes.
* [ ] Build a Canvas UI dashboard in Unity to display raw telemetry numbers alongside the 3D visualizations.
