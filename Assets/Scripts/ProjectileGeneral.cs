using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGeneral : MonoBehaviour
{
    protected float speed;
    protected GameManager gameManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
        if (transform.position.z < -25 || transform.position.z > 10 || transform.position.x > 16 || transform.position.x < -16)
        {
            Destroy(gameObject);
        }
    }
}
