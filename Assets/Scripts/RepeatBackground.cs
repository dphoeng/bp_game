using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float speed = 4;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (transform.position.z < startPos.z - 40)
            {
                transform.position = startPos;
            }
        }
    }
}
