using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackgroundObjects : MonoBehaviour
{
    private float minPositionX = -8f;
    private float maxPositionX = 8f;
    private float minScale = 0.5f;
    private float maxScale = 6f;
    private int spawnChance = 240;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // TODO: maybe something like a seed to keep gameplay consistent without having to generate things manually
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive)
        {
            if (Random.Range(0, spawnChance) == 1)
            {
                float lerp = Mathf.PingPong(Time.time, 1f) / 1f;
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(Random.Range(minPositionX, maxPositionX), 0, 10);
                cube.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
                cube.AddComponent<MoveBackgroundObjects>();
                cube.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f);

            }
        }
    }
}
