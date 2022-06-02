using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> bossPrefabs;
    public TextMeshProUGUI bossText;
    public GameObject bossScreen;
    public Image maskBoss;
    public Image fillBoss;
    private float spawnIntervalMin = 2f;
    private float spawnIntervalMinMin = 0.05f;
    private float spawnIntervalMax = 5f;
    private float spawnIntervalMaxMin = 0.5f;
    private float spawnIntervalMinInc = 1f;
    private float startTime;
    private float delay = 0.0f;
    private float delayInc = 0.0f;
    private int enemySpawn;
    private int bossSpawn;
    private List<float> enemyAllowedTime = new List<float> { 30f, 60f, 160f };
    private List<string> bossNames = new List<string> { "Big Boss Luigi" };
    private List<float> bossAllowedTime = new List<float> { 120f };
    private List<bool> enemyPastTime;
    private List<bool> bossPastTime;
    private int index = 0;

    // time within boos phase;
    private float bossTimePast;

    // time past ingame outside boss, used to scale hp and determine possible enemies + spawns boss
    public float timePast;

    // DEBUG SCREEN
    public TextMeshProUGUI intervalText;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 0;
        enemySpawn = 0;
        bossSpawn = 0;
        enemyPastTime = new List<bool> { false, false, false };
        bossPastTime = new List<bool> { false };
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive)
        {
            if (!GameObject.FindGameObjectWithTag("Boss"))
            {
                bossScreen.SetActive(false);
                timePast = Time.time - startTime - bossTimePast;
			    intervalText.text = "spawnIntervalMin<br>" + spawnIntervalMin + "<br><br>spawnIntervalMax<br>" + spawnIntervalMax + "<br><br>timePast<br>" + timePast;
                if (delayInc <= Time.time)
                {
                    if (spawnIntervalMin > spawnIntervalMinMin)
                    {
                        spawnIntervalMin += -0.01f;
                    }
                    else
                    {
                        spawnIntervalMin = spawnIntervalMinMin;
                    }
                    if (spawnIntervalMax > spawnIntervalMaxMin)
                    {
                        // slow the difficulty increase down (not really since it's not linear in the first place, but without this the curve is insane)
                        if (spawnIntervalMax < 2 && spawnIntervalMax >= 1.25f)
                            spawnIntervalMax -= 0.00625f;
                        else if (spawnIntervalMax < 1.25f && spawnIntervalMax >= 1)
                            spawnIntervalMax -= 0.002f;
                        else if (spawnIntervalMax < 1)
                            spawnIntervalMax -= 0.001f;
                        else
                            spawnIntervalMax -= 0.025f;
                    }
                    else
                    {
                        spawnIntervalMax = spawnIntervalMaxMin;
                    }
                    delayInc = Time.time + spawnIntervalMinInc;
                }
                if (enemyPrefabs.Count - 1 > enemySpawn)
                {
                    if (timePast > enemyAllowedTime[enemySpawn] && !enemyPastTime[enemySpawn])
                    {
                        enemyPastTime[enemySpawn] = true;
                        enemySpawn++;
                    }
                }
                if (bossPrefabs.Count > bossSpawn)
                {
                    if (timePast > bossAllowedTime[bossSpawn] && !bossPastTime[bossSpawn])
                    {
                        Instantiate(bossPrefabs[bossSpawn], new Vector3(0, 5, 5), transform.rotation);
                        bossPastTime[bossSpawn] = true;
                        bossText.text = bossNames[bossSpawn];
                        bossScreen.SetActive(true);
                        bossSpawn++;
                    }
                }
                if (delay <= Time.time)
                {
                    int res = Mathf.FloorToInt(gameManager.randomList[index] / (1000 / (enemySpawn + 1)));
                    index = gameManager.randomList.Count > index + 1 ? index + 1 : 0;
                    Instantiate(enemyPrefabs[res], new Vector3(GetRandomFromSeed(-7, 7), 5, 5), transform.rotation);
                    delay = Time.time + GetRandomFromSeed(spawnIntervalMax, spawnIntervalMin);
                }
            }
            else
            {
                bossTimePast += Time.deltaTime;
                maskBoss.fillAmount = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().getHitpoints() / GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().hitpointsStart;
                bossText.color = new Color(Wave(1.5f, Time.time, 0), Wave(1.5f, Time.time, 2), Wave(1.5f, Time.time, 4));
                fillBoss.GetComponent<Image>().color = new Color(Wave(1.5f, Time.time, 0), Wave(1.5f, Time.time, 2), Wave(1.5f, Time.time, 4));
            }
        }
    }

    public void StartSpawn()
    {
        startTime = Time.time;
        index = 990;
    }

    private float GetRandomFromSeed(float min, float max)
    {
        float ret = gameManager.randomList[index] == 0 ? min : (max - min) / gameManager.randomList.Count * gameManager.randomList[index] + min;
        index = gameManager.randomList.Count > index + 1 ? index + 1 : 0;
        return ret;
    }

    //public void SpawnWave()
    //{
    //    for (int i = 0; i < 7; i++)
    //    {
    //        if (i < 4)
    //            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector3(-4.5f + i * 1.5f, 5, 10 - 2f * i), transform.rotation);
    //        else
    //            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector3(-4.5f + i * 1.5f, 5, -2 + 2f * i), transform.rotation);
    //    }
    //}

    private float Wave(float lambda, float i, int add)
    {
        return 0.5f * Mathf.Sin(2 * Mathf.PI / lambda * i + add) + 0.5f;
    }
}
