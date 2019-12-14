using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    private WorldController world;
    private Water2D.Water2D_Spawner spawner;
    private Collider2D col;

    public float windStrength = 0;

    // Start is called before the first frame update
    void Start()
    {

        world = GetComponentInParent<WorldController>();
        spawner = world.GetComponentInChildren<Water2D.Water2D_Spawner>();
        col = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        windStrength = 1000 * MicInput.MicLoudness;
        Debug.Log(windStrength);
        if (windStrength > 1)
        {
            windStrength = 1;
        }

        foreach (GameObject o in spawner.WaterDropsObjects)
        {
            if (col.OverlapPoint(o.transform.position))
            {
                float angle = transform.localEulerAngles.z;
                angle *= Mathf.PI / 180.0f;
                Debug.Log(angle);
                o.GetComponent<Rigidbody2D>().velocity += new Vector2(-1 * Mathf.Sin(angle), 1 * Mathf.Cos(angle)) * windStrength;
            }
        }

    }
}
