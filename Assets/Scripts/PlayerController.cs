using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float delay = 0.0f;
    private float delay2 = 0.0f;
    private float delay3 = 0.0f;
    private float ringDelay = 0.0f;
    private float ringDelayLimit = 1f;
    private GameManager gameManager;
    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;
    public GameObject projectilePrefab3;
    public GameObject laserPrefab;
    public GameObject clearRingPrefab;
    private GameObject lastProjectile;
    public bool touchedBoss;
    public bool lockMovement;
    public float touchedBossTime;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        touchedBoss = false;
        lockMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        // enable debug screen
        if (Input.GetKey(KeyCode.Y))
        {
            if (Input.GetKey(KeyCode.U))
            {
                if (Input.GetKey(KeyCode.H))
                    gameManager.debugScreen.SetActive(true);
            }
        }

        if (gameManager.gameActive)
        {
            if (Input.GetKey(KeyCode.L))
            {
                if (Input.GetKeyDown(KeyCode.P))
                    transform.GetComponent<PlayerStats>().LevelUp();
            }

            if (!lockMovement)
            {
                transform.Translate(speed * Time.deltaTime * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")), Space.World);
                if (Input.GetKey(KeyCode.Z))
                {
                    transform.Rotate(700 * Time.deltaTime * new Vector3(0, -0.4f, 0));
                }
                if (Input.GetKey(KeyCode.C))
                {
                    transform.Rotate(700 * Time.deltaTime * new Vector3(0, 0.4f, 0));
                }

                // Shoot projectiles
                if (Input.GetKey(KeyCode.Space))
                {
                    if (delay <= Time.time)
                    {
                        if (gameManager.playerStats.Level < 4)
                            Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180f, 0));
                        else if (gameManager.playerStats.Level >= 4)
                        {
                            lastProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180f, 0));
                            lastProjectile.transform.Translate(new Vector3(0.3f, 0, 0), Space.Self);
                            lastProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180f, 0));
                            lastProjectile.transform.Translate(new Vector3(-0.3f, 0, 0), Space.Self);

                        }
                        if (gameManager.playerStats.Level >= 10)
                        {
                            lastProjectile = Instantiate(projectilePrefab3, transform.position, transform.rotation * Quaternion.Euler(0, 205f, 0));
                            lastProjectile.transform.Translate(new Vector3(-0.3f, 0, 0), Space.Self);
                            lastProjectile = Instantiate(projectilePrefab3, transform.position, transform.rotation * Quaternion.Euler(0, 155f, 0));
                            lastProjectile.transform.Translate(new Vector3(0.3f, 0, 0), Space.Self);
                        }
                        delay = Time.time + gameManager.playerStats.AttackSpeed;
                    }
                    if (delay2 <= Time.time)
                    {
                        if (gameManager.playerStats.Level >= 13)
                        {
                            Instantiate(projectilePrefab2, transform.position, transform.rotation * Quaternion.Euler(0, 180f, 0));
                        }
                        delay2 = Time.time + gameManager.playerStats.AttackSpeed2;
                    }
                    if (delay3 <= Time.time)
                    {
                        if (gameManager.playerStats.Level >= 7)
                        {
                            Instantiate(laserPrefab, transform.position, transform.rotation);
                        }
                        delay3 = Time.time + gameManager.playerStats.AttackSpeed3;
                    }
                }

                // Slow player down for more control and precise movement
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 2f;
                }
                else
                {
                    speed = 5f;
                }

                // Spawn nuke
                if (Input.GetKey(KeyCode.X))
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
        if (touchedBoss)
        {
            if (touchedBossTime > Time.time)
                transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Boss").transform.position, speed * Time.deltaTime * -1 * 2);
            else
                touchedBoss = false;
        }
    }

    public void Nuke()
    {
        Instantiate(clearRingPrefab, transform.position, transform.rotation);
    }
}
