using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public List<GameObject> testPrefab;
    private int spawnInterval = 150;
    private float spawnIntervalMin = 0f;
    private float delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive)
        {
            if (delay <= Time.time)
            {
                if (Random.Range(0, spawnInterval) == 1)
                {
                    Instantiate(testPrefab[Random.Range(0, testPrefab.Count)], new Vector3(Random.Range(-7, 7), 5, 5), transform.rotation);
                    delay = Time.time + spawnIntervalMin;
                }
            }
        }
        // TODO: spawn enemies according to seed
    }

    public void SpawnWave()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i < 4)
                Instantiate(testPrefab[Random.Range(0, testPrefab.Count)], new Vector3(-4.5f + i * 1.5f, 5, 10 - 2f * i), transform.rotation);
            else
                Instantiate(testPrefab[Random.Range(0, testPrefab.Count)], new Vector3(-4.5f + i * 1.5f, 5, -2 + 2f * i), transform.rotation);
        }
    }
}
