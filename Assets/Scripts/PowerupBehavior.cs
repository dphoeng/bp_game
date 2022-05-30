using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    private float speedLimit = 3f;
    private float speedStart = -3f;
    protected GameManager gameManager;
    private float start;

    // Start is called before the first frame update
    void Start()
    {    
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 2 * Mathf.Pow(Time.time - start, 2) + speedStart;
        if (speed > speedLimit)
            speed = speedLimit;
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Outer Hitbox"))
        {
            if (gameObject.name == "Experience Cube(Clone)")
            {
                Destroy(gameObject);
                gameManager.playerStats.AddXp(10f);
            } else
            {
                Destroy(gameObject);
                gameManager.playerStats.AddBombPrg(4f);
            }
        }
    }
}
