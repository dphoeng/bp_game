using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public List<GameObject> enemyPrefabs;
    private float spawnInterval;
    private float spawnIntervalMin = 0f;
    private float spawnIntervalMinInc = 1f;
    private float startTime;
    private float delay = 0.0f;
    private float delayInc = 0.0f;
    private int enemySpawn;
    private List<float> enemyAllowedTime = new List<float> { 30f, 60f };
    private List<bool> enemyPastTime;
    private int x;

    // DEBUG SCREEN
    public TextMeshProUGUI intervalText;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 0;
        spawnInterval = 1200;
        enemySpawn = 0;
        x = 0;
        enemyPastTime = new List<bool> { false, false };
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive)
        {
            float timePast = Time.time - startTime;
            intervalText.text = "spawnInterval<br>" + spawnInterval;
            if (delayInc <= Time.time)
            {
                if (spawnInterval > 300)
                {
                    spawnInterval += -0.0002f * Mathf.Pow(x, 2) - 4;
                    x++;
                }
                else
                {
                    spawnInterval = 300;
                }
                delayInc = Time.time + spawnIntervalMinInc;
            }
            if (enemyPrefabs.Count - 1> enemySpawn)
            {
                if (timePast > enemyAllowedTime[enemySpawn] && !enemyPastTime[enemySpawn])
                {
                    enemyPastTime[enemySpawn] = true;
                    enemySpawn++;
                }
            }
            if (delay <= Time.time)
            {
                if (Random.Range(0, spawnInterval) <= 1)
                {
                    Instantiate(enemyPrefabs[Random.Range(0, enemySpawn + 1)], new Vector3(Random.Range(-7f, 7f), 5, 5), transform.rotation);
                    delay = Time.time + spawnIntervalMin;
                }
            }
        }
    }

    public void startSpawn()
    {
        startTime = Time.time;
    }

    public void SpawnWave()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i < 4)
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector3(-4.5f + i * 1.5f, 5, 10 - 2f * i), transform.rotation);
            else
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector3(-4.5f + i * 1.5f, 5, -2 + 2f * i), transform.rotation);
        }
    }
}
