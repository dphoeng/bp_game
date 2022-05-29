using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    protected float hitpoints;
    protected float speed;
    protected float shootInterval;
    protected int scoreAtKill;
    protected int xpAtKill;
    protected int bombAtKill;
    protected float delay = 0.0f;
    protected Vector3 rotation;
    protected GameObject experiencePrefab;
    protected GameObject bombPrefab;
    protected GameObject projectilePrefab;
    protected GameManager gameManager;
    protected SpawnManager spawnManager;
    public int lastFrame = 0;

    // Start is called before the first frame update, after child object's start
    protected virtual void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * 500);
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.playerStats.LoseLive();
            if (gameManager.playerStats.Lives < 0)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }

    public float getHitpoints()
    {
        return hitpoints;
    }

    public void takeDamage(float damage, int frame)
    {
        if (frame != lastFrame)
        {
            lastFrame = frame;
            hitpoints -= damage;
            if (hitpoints <= 0)
            {
                for (int i = 0; i < xpAtKill; i++)
                {
                    Vector3 newPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    newPos.x = Mathf.Min(8, Mathf.Max(-8, newPos.x));
                    Instantiate(experiencePrefab, newPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                }

                for (int i = 0; i < bombAtKill; i++)
                {
                    Vector3 newPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    newPos.x = Mathf.Min(8, Mathf.Max(-8, newPos.x));
                    Instantiate(bombPrefab, newPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                Destroy(gameObject);
                gameManager.playerStats.AddScore(scoreAtKill);
            }
        }
    }
}
