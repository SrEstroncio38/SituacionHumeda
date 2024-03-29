﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{

    public bool autoStart = false;

    [Header("UI")]
    public UnityEngine.UI.Button playButton;
    public Sprite restartImage;
    public Sprite restartImageP;
    public GameObject levelComplete;
    public GameObject pauseMenu;

    private Water2D.Water2D_Spawner spawner;
    private bool _started = false;

    [ReadOnly]
    public bool gyroAvailable = false;
    public Vector2 gravity;
    public float magnitude;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1.0f;
        spawner = GetComponentInChildren<Water2D.Water2D_Spawner>();
        gyroAvailable = EnableGyro();
        if (levelComplete != null)
            levelComplete.SetActive(false);
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        if (autoStart)
        {
            spawner.Spawn();
        }
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
        float y = gravity.y;
        if (y > 0)
        {
            y = 0;
        }
        gravity = new Vector2(gravity.x, y);
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
            playButton.GetComponent<UnityEngine.UI.Image>().sprite = restartImage;
            UnityEngine.UI.SpriteState ss = playButton.spriteState;
            ss.pressedSprite = restartImageP;
            playButton.spriteState = ss;
        }
        else
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void CloseGame()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
        else
        {
            Application.Quit();
        }
    }

}
