using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameActive = false;
    public GameObject seedInput;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public Button startButton;
    public Button restartButton;
    public SpawnManager spawnManager;
    public TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        score = 0;
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        int seed;

        gameActive = true;

        if (int.TryParse(seedInput.GetComponent<TMP_InputField>().text, out seed))
		{
            Random.InitState(seed);
        } else
		{
            Random.InitState(0);
        }
        // spawnManager.SpawnWave();
        titleScreen.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameActive = false;
        gameOverScreen.gameObject.SetActive(true);
        // TODO: destroy all objects on screen?
    }

    public void addScore(int scoreToAdd)
	{
        score += scoreToAdd;
        scoreText.text = score + "";

    }

    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
