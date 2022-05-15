using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundObjects : MonoBehaviour
{
    private float speed = 4f;
    private GameManager gameManager;

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
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (transform.position.z < -25)
		    {
                Destroy(gameObject);
		    }
        }
    }
}
