using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{

    [Header("UI")]
    public UnityEngine.UI.Text playText;
    public GameObject levelComplete;

    [Header("Scenes")]
    public Scene nextLevel;
    public Scene backScene;

    private Water2D.Water2D_Spawner spawner;
    private bool _started = false;

    [ReadOnly]
    public bool gyroAvailable = false;
    public Vector2 gravity;
    public float magnitude;

    // Start is called before the first frame update
    void Start() {
        spawner = GetComponentInChildren<Water2D.Water2D_Spawner>();
        gyroAvailable = EnableGyro();
        levelComplete.SetActive(false);
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

    public void StartGame()
    {
        if (!_started)
        {
            spawner.Spawn();
            _started = true;
            playText.text = "Restart";
        }
        else
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    public void GoToNextLevel()
    {
        if (nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel.name);
        }
    }

    public void GoToMenu()
    {
        if (backScene != null)
        {
            SceneManager.LoadScene(backScene.name);
        }
    }
}
