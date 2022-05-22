using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float delay = 0.0f;
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
        // enable debug screen
        if (Input.GetKey("y"))
        {
            if (Input.GetKey("u"))
            {
                if (Input.GetKey("h"))
                    gameManager.debugScreen.SetActive(true);
            }
        }

        if (gameManager.gameActive)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime);

            // Shoot projectiles
            if (Input.GetKey("space"))
            {
                if (delay <= Time.time)
                {
                    if (gameManager.playerStats.Level < 4)
                        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation * Quaternion.Euler(0, 180f, 0));
                    else if (gameManager.playerStats.Level < 8)
                    {
                        Instantiate(projectilePrefab, transform.position + new Vector3(0.3f, 0, 0), projectilePrefab.transform.rotation * Quaternion.Euler(0, 180f, 0));
                        Instantiate(projectilePrefab, transform.position + new Vector3(-0.3f, 0, 0), projectilePrefab.transform.rotation * Quaternion.Euler(0, 180f, 0));
                    } else
                    {
                        Instantiate(projectilePrefab, transform.position + new Vector3(0.3f, 0, 0), projectilePrefab.transform.rotation * Quaternion.Euler(0, 195f, 0));
                        Instantiate(projectilePrefab, transform.position + new Vector3(0, 0, 0), projectilePrefab.transform.rotation * Quaternion.Euler(0, 180f, 0));
                        Instantiate(projectilePrefab, transform.position + new Vector3(-0.3f, 0, 0), projectilePrefab.transform.rotation * Quaternion.Euler(0, 165f, 0));
                    }
                    delay = Time.time + gameManager.playerStats.AttackSpeed;
                }
            }

            // Slow player down for more control and precise movement
            if (Input.GetKey("left shift"))
            {
                speed = 2f;
            }
            else
            {
                speed = 5f;
            }

            // Spawn nuke
            if (Input.GetKey("x"))
            { 
                if (ringDelay <= Time.time)
                {
                    if (gameManager.playerStats.BombCount > 0)
					{
                        Nuke();
                        gameManager.playerStats.AddBomb(-1);
                        ringDelay = Time.time + ringDelayLimit;
					} else
					{
                        gameManager.playerStats.NoBombs();
					}
                }
            }

            // Border control
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

    public void Nuke()
    {
        Instantiate(clearRingPrefab, transform.position, transform.rotation);
    }
}
