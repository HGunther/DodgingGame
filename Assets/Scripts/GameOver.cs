using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text secondsSurvivedUI;
    bool gameOver;

    int touchCount = int.MaxValue;

    private void Start()
    {
        FindObjectOfType<PlayerControls>().PlayerDestroyed += OnGameOver;

    }
    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > touchCount)
            {
                SceneManager.LoadScene(0);
            }
            touchCount = Input.touchCount;
        }
    }

    void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        secondsSurvivedUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
        gameOver = true;
        touchCount = int.MaxValue;
    }
}
