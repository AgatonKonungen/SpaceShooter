using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public int score;

    public GameObject playerExplosion;

    public bool isExplosionSet;

    public Text scoreText;

    public Canvas gameplayCanvas;

    public Canvas gameoverCanvas;

    public Text Gameovertext;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        gameoverCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if (isExplosionSet && !playerExplosion && Time.fixedTime != 0)
        {
            Gameovertext.text = "Your score is: " + score.ToString();
            gameplayCanvas.enabled = false;
            gameoverCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void PressingRetry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PressingExit()
    {
        Application.Quit();
    }
}
