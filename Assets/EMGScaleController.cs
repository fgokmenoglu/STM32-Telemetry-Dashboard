using UnityEngine;
using System.IO.Ports;

public class EMGScaleController : MonoBehaviour
{
    [Header("Connection Settings")]
    public string portName = "/dev/ttyACM0";
    public int baudRate = 115200;

    private SerialPort data_stream;
    
    [Header("Sphere Scaling")]
    public float minScale = 1.0f;  // Size of sphere when relaxed
    public float maxScale = 4.0f;  // Size of sphere at maximum flex
    
    [Header("Sensor Calibration")]
    public float adcBaseline = 1861f; // 1.5V Standby
    public float adcMax = 4095f;      // 3.3V Active
    
    private float targetScale;

    void Start()
    {
        targetScale = minScale;
        // Initialize the serial port using the variables from the Inspector
        data_stream = new SerialPort(portName, baudRate);
        data_stream.Open();
        data_stream.ReadTimeout = 10;
    }

    void Update()
    {
        if (data_stream.IsOpen)
        {
            try
            {
                string dataString = data_stream.ReadLine();
                if (float.TryParse(dataString, out float rawAdcValue))
                {
                    // 1. Clamp the value 
                    rawAdcValue = Mathf.Clamp(rawAdcValue, adcBaseline, adcMax);
                    
                    // 2. Normalize the reading 
                    float normalizedFlex = (rawAdcValue - adcBaseline) / (adcMax - adcBaseline);
                    
                    // 3. Map that percentage to our physical scale limits
                    targetScale = Mathf.Lerp(minScale, maxScale, normalizedFlex);
                }
            }
            catch (System.TimeoutException) 
            { 
                // Ignore timeouts
            }
        }

        // 4. Smoothly animate the sphere
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, Time.deltaTime * 10f);
    }

    void OnApplicationQuit()
    {
        if (data_stream != null && data_stream.IsOpen)
        {
            data_stream.Close();
        }
    }
}