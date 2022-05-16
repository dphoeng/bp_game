using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject testPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: spawn enemies according to seed
    }

    public void SpawnWave()
    {

    }

    public void EnemySpawn()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i < 4)
                Instantiate(testPrefab, new Vector3(-4.5f + i*1.5f, 5, 14 - 2f*i), transform.rotation);
            else
                Instantiate(testPrefab, new Vector3(-4.5f + i * 1.5f, 5, 2 + 2f * i), transform.rotation);
        }

    }
}
