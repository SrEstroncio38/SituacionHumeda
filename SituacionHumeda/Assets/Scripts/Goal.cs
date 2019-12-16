using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public Water2D.Water2D_Spawner spawner;

    private BoxCollider2D goal;
    private WorldController world;

    // Start is called before the first frame update
    void Start()
    {
        goal = GetComponentInChildren<BoxCollider2D>();
        world = GetComponentInParent<WorldController>();
    }

    // Update is called once per frame
    void Update()
    {
        int numParticles = 0;
        foreach (GameObject o in spawner.WaterDropsObjects)
        {
            if (goal.OverlapPoint(o.transform.position))
            {
                numParticles++;
            }
        }
        
        if (numParticles >= 50)
        {
            CompleteLevel();
        }

    }

    private void CompleteLevel()
    {

        world.levelComplete.SetActive(true);
        Time.timeScale = 0.0f;

    }

}
