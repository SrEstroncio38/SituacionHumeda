using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{

    public bool gyroAvailable = false;

    private UnityEngine.Gyroscope gyro;

    // Start is called before the first frame update
    void Start() { 
    
        gyroAvailable = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = gyro.gravity * 9.81f;
        Debug.Log(gyro.rotationRate);
    }
}
