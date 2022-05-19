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
    public GameObject scoreScreen;
    public Button startButton;
    public Button restartButton;
    public SpawnManager spawnManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bombText;
    private int score;
    private float scoreInterval = 1f;
    private float delay;
    private float experience;
    private int level;
    private float bombProgress;
    private int bombCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        score = 0;
        experience = 0;
        level = 1;
        bombProgress = 0;
        bombCount = 2;
        scoreText.text = "Score<br>0";
        levelText.text = "Level<br>1 - 0%";
        bombText.text = "Bombs<br>2 - 0%";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            if (delay <= Time.time)
            {
                AddScore(10);
                delay = scoreInterval + Time.time;
            }
        }

        if (bombText.color != new Color(1, 1, 1))
		{
            bombText.color += new Color(0, 0.004f, 0.004f);
		}
    }

    public void StartGame()
    {
        gameActive = true;

        if (int.TryParse(seedInput.GetComponent<TMP_InputField>().text, out int seed))
		{
            Random.InitState(seed);
        } else
		{
            Random.InitState(0);
        }
        // spawnManager.SpawnWave();
        titleScreen.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gameActive = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void AddScore(int scoreToAdd)    
	{
        score += scoreToAdd;
        scoreText.text = "Score<br>" + score;
    }
    public void AddXp(float xpToAdd)
    {
        experience += xpToAdd;
        if (experience >= 100f)
        {
            experience -= 100f;
            LevelUp();
        }
        levelText.text = "Level<br>" + level + " - " + experience + "%";
    }
    public void AddBombPrg(float vToAdd)
    {
        bombProgress += vToAdd;
        if (bombProgress >= 100f)
        {
            bombProgress -= 100f;
            AddBomb(1);
        }
        levelText.text = "Level<br>" + level + " - " + experience + "%";
    }

    public void AddBomb(int count)
	{
        bombCount += count;
        bombText.text = "Bombs<br>" + bombCount + " - " + bombProgress + "%";
	}

    public void NoBombs()
	{
        bombText.color = Color.red;
	}

    public int GetBombCount()
    {
        return bombCount;
    }

    private void LevelUp()
    {
        level += 1;
    }

    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
