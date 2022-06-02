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
    public GameObject debugScreen;
    public GameObject livesPrefab;
    public List<GameObject> livesList;
    public PlayerController player;
    public PlayerStats playerStats;
    public Button startButton;
    public Button restartButton;
    public SpawnManager spawnManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI seedText;
    public TextMeshProUGUI fpsText;
    public GameObject livesParent;
    public Image maskLevel;
    public Image maskBomb;
    private float delay = 0;
    private float scoreInterval = 1f;
    private float fpsRefreshRate = 0.5f;
    private float delayFps = 0;
    public List<int> randomList;
    public int globalIndex = 0;

    // TODO LIST
    //      Add more level guns: 4
    //      Add more enemies: 4
    //      Add boss: 1
    //      pause screen?
    //      SOUNDS!

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);

        scoreText.text = "Score<br>0";
        levelText.text = "Lvl. 1";
        bombText.text = playerStats.BombCount + "";
        playerStats.AddLives(2);
        maskLevel.fillAmount = 0;
        maskBomb.fillAmount = 0;
        debugScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (delayFps < Time.time)
        {
            fpsText.text = 1 / Time.unscaledDeltaTime + " FPS";
            delayFps = Time.time + fpsRefreshRate;
        }
        if (gameActive)
        {
            if (delay <= Time.time)
            {
                playerStats.AddScore(10);
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
            seedText.text = "Seed<br>" + seed;
        }
        else
        {
            Random.InitState(0);
            seedText.text = "Seed<br>0";
        }
        List<int> intList = new List<int>();
        for (int i = 0; i < 1000; i++)
        {
            intList.Add(Random.Range(0, 1000));
        }

        randomList = intList;

        spawnManager.StartSpawn();
        titleScreen.SetActive(false);
        scoreScreen.SetActive(true);
    }

    public void GameOver()
    {
        gameActive = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public IEnumerator FlashPlayer()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                player.GetComponent<MeshRenderer>().enabled = false;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                player.GetComponent<MeshRenderer>().enabled = true;
                yield return new WaitForSeconds(0.4f);
            }
        }
    }

    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
