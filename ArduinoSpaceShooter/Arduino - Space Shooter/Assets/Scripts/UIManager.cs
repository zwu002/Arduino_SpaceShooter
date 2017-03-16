using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    Scene currentScene;

    public Text scoreText;
    public Text gameOverText;
    public Text gameOverTextII;
    public int score;

    public bool gameOver;

	// Use this for initialization
	void Start () {
        gameOver = false;
        Time.timeScale = 1;
        score = 0;
        currentScene = SceneManager.GetActiveScene();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;

        if (gameOver == true)
        {
            gameOverActivate();
        }
    }

    public void scoreUpdate()
    {
        score++;
    }

    public void gameOverActivate()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverTextII.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene(currentScene.name);
    }

}
