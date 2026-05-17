using UnityEngine;
using System.IO.Ports;
using System;

public class DialController : MonoBehaviour
{
    // Define the serial port matching your STM32 setup
    SerialPort dataStream = new SerialPort("COM3", 115200);

    void Start()
    {
        // A short timeout is CRITICAL so Unity doesn't freeze waiting for data
        dataStream.ReadTimeout = 10; 
        
        try
        {
            dataStream.Open();
            Debug.Log("Port Opened Successfully!");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to open port: " + e.Message);
        }
    }

    void Update()
    {
        if (dataStream.IsOpen)
        {
            try
            {
                // Read the incoming string until it hits the \n character
                string incomingData = dataStream.ReadLine();
                
                // Parse the string into a float
                if (float.TryParse(incomingData, out float parsedAngle))
                {
                    // Apply the absolute angle directly to the 3D model's Y-axis
                    transform.localEulerAngles = new Vector3(0, parsedAngle, 0);
                }
            }
            catch (TimeoutException)
            {
                // Normal behavior if the buffer is temporarily empty; just skip to the next frame
            }
            catch (Exception e)
            {
                Debug.LogWarning("Serial read glitch: " + e.Message);
            }
        }
    }

    // This prevents the COM port from hanging when you stop the Unity player
    void OnApplicationQuit()
    {
        if (dataStream != null && dataStream.IsOpen)
        {
            dataStream.Close();
        }
    }
}