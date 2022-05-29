using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackgroundObjects : MonoBehaviour
{
    private float minPositionX = -8f;
    private float maxPositionX = 8f;
    private float minScale = 0.5f;
    private float maxScale = 6f;
    private float spawnTimeMax = 1f;
    private float spawnTimeMin = 0.05f;
    private float delay;
    private GameManager gameManager;
    private int index;

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
            if (delay < Time.time)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(GetRandomFromSeed(minPositionX, maxPositionX), 0, 10);
                cube.transform.localScale = new Vector3(GetRandomFromSeed(minScale, maxScale), GetRandomFromSeed(minScale, maxScale), GetRandomFromSeed(minScale, maxScale));
                cube.AddComponent<MoveBackgroundObjects>();
                cube.GetComponent<Renderer>().material.color = new Color(GetRandomFromSeed(0, 255) / 255f, GetRandomFromSeed(0, 255) / 255f, GetRandomFromSeed(0, 255) / 255f);
                delay = Time.time + GetRandomFromSeed(spawnTimeMin, spawnTimeMax);
            }
        }
    }

    private float GetRandomFromSeed(float min, float max)
    {
        float ret = gameManager.randomList[index] == 0 ? min : (max - min) / gameManager.randomList.Count * gameManager.randomList[index] + min;
        index = gameManager.randomList.Count > index + 1 ? index + 1 : 0;
        return ret;
    }
}
