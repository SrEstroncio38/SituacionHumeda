using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    [ReadOnly]
    public bool gyroAvailable = false;
    public Vector2 gravity;
    public float magnitude;

    // Start is called before the first frame update
    void Start() { 
    
        gyroAvailable = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        gravity = new Vector2(Input.gyro.gravity.x, Input.gyro.gravity.y);
        gravity.Normalize();
        gravity *= 9.81f;
        magnitude = gravity.magnitude;
        Physics2D.gravity = gravity;
    }
}
