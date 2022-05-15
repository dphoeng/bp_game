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

    public void EnemySpawn()
    {
        Instantiate(testPrefab, new Vector3(0, 5, 8), transform.rotation);
    }
}
