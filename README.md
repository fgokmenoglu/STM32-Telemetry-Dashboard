# STM32 to Unity: Digital Twin Telemetry Bridge

A lightweight Hardware-in-the-Loop (HIL) prototype demonstrating real-time serial communication between an STM32 microcontroller and the Unity Engine. 

This project bridges the gap between embedded test automation and 3D industrial simulation, proving out the data pipeline required for rendering physical hardware states in a digital environment.

## 🛠️ Tech Stack
* **Hardware:** NUCLEO-G491RE (ARM Cortex-M4)
* **Firmware:** Embedded C via STM32CubeIDE (HAL Drivers)
* **Engine:** Unity 2022.3 LTS (Built-In Render Pipeline)
* **Scripting:** C# (.NET Framework)

## ⚙️ How It Works
1. **The Synthetic Sensor:** The STM32 generates a continuous, sweeping synthetic telemetry signal (0-358 degrees) to simulate a physical rotary encoder.
2. **LPUART Routing:** Data is formatted with carriage returns (`\r\n`) and streamed over the G491RE's Low-Power UART (`LPUART1`) via the ST-LINK Virtual COM Port at 115200 baud.
3. **The C# Serial Bridge:** A highly optimized Unity script (`DialController.cs`) captures the live COM port data, parses the string, and applies the absolute angle directly to a 3D cylinder's transform matrix in real-time.

## 🚀 Next Steps
* Integrate a physical sensor (e.g., potentiometer, IMU, or depth vision camera).
* Expand the telemetry packet to include multi-axis data and system diagnostics.
