using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    private float speed = 3f;
    protected GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {    
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Outer Hitbox"))
        {
            Destroy(gameObject);
            gameManager.AddXp(10f);

        }
    }
}
