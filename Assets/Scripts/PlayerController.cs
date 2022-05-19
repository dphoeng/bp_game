using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float delay = 0.0f;
    private float delayLimit = 0.25f;
    private float ringDelay = 0.0f;
    private float ringDelayLimit = 1f;
    private GameManager gameManager;
    public GameObject projectilePrefab;
    public GameObject clearRingPrefab;

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
            if (Input.GetKey("up"))
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Input.GetKey("down"))
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (Input.GetKey("left"))
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (Input.GetKey("right"))
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (Input.GetKey("space"))
            {
                if (delay <= Time.time)
                {
                    Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation * Quaternion.Euler(0, 180f, 0));
                    delay = Time.time + delayLimit;
                }
            }
            if (Input.GetKey("left shift"))
            {
                speed = 2f;
            }
            else
            {
                speed = 5f;
            }
            if (Input.GetKey("x"))
            { 
                if (ringDelay <= Time.time)
                {
                    if (gameManager.GetBombCount() > 0)
					{
                        Instantiate(clearRingPrefab, transform.position, transform.rotation);
                        gameManager.AddBomb(-1);
                        ringDelay = Time.time + ringDelayLimit;
					} else
					{
                        gameManager.NoBombs();
					}
                }
            }
            if (transform.position.z < -17.5f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -17.5f);
            if (transform.position.z > -2.5f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
            if (transform.position.x > 7.5f)
                transform.position = new Vector3(7.5f, transform.position.y, transform.position.z);
            if (transform.position.x < -7.5f)
                transform.position = new Vector3(-7.5f, transform.position.y, transform.position.z);

        }
    }
}
