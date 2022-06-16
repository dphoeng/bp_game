using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2LaserSpawn : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.GetComponent<Renderer>().material.color.a > 0.85f)
        {
            gameManager.playerStats.LoseLive();
            if (gameManager.playerStats.Lives < 0)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
            // TODO: When hit, lose some xp + drop some of that on the field
        }
    }
}