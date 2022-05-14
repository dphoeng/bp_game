using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackgroundObjects : MonoBehaviour
{
    private float speed = 4f;
    private float minPositionX = -8f;
    private float maxPositionX = 8f;
    private float minScale = 0.5f;
    private float maxScale = 6f;
    private int spawnChance = 240;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, spawnChance) == 1)
		{
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(Random.Range(minPositionX, maxPositionX), 0, 10);
            cube.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
            cube.AddComponent<MoveBackgroundObjects>();
        }
    }
}
