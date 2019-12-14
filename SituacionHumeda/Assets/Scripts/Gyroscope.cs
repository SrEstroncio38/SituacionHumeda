using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{

    public bool gyroAvailable = false;

    private UnityEngine.Gyroscope gyro;

    // Start is called before the first frame update
    void Start()
    {
        gyro = Input.gyro;
        gyroAvailable = SystemInfo.supportsGyroscope;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = gyro.gravity;
        Debug.Log(gyro.rotationRate);
    }
}
