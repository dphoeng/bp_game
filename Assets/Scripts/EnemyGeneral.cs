using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    protected float hitpoints;
    protected float startingHitpoints;
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
    private int index;
    protected int lastLaserHit;

    // Start is called before the first frame update, after child object's start
    protected virtual void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        index = gameManager.globalIndex;
        int indexToAdd = (xpAtKill + bombAtKill) * 2;
        gameManager.globalIndex = gameManager.randomList.Count > index + indexToAdd ? index + indexToAdd : indexToAdd - (gameManager.randomList.Count - index);
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player Projectile"))
        {
            TakeDamage(other.GetComponent<ProjectileGeneral>().damage, Time.frameCount);
            Destroy(other.gameObject);
        }
        // if hit by player laser, check if current position is inside field, check whether this object has already been hit by this laser already and check whether the alpha value of the laser is higher than 0.95 (fresh laser)
        else if (other.transform.CompareTag("Player Laser") && transform.position.z < 0 && lastLaserHit != other.gameObject.GetInstanceID() && other.gameObject.GetComponent<Renderer>().material.color.a > 0.95f)
        {
            TakeDamage(other.GetComponent<ProjectileGeneral>().damage, Time.frameCount);
        }
    }

    public float getHitpoints()
    {
        return hitpoints;
    }

    public void TakeDamage(float damage, int frame)
    {
        if (frame != lastFrame)
        {
            lastFrame = frame;
            hitpoints -= damage;
            if (hitpoints <= 0)
            {
                for (int i = 0; i < xpAtKill; i++)
                {
                    Vector3 newPos = transform.position + new Vector3(GetRandomFromSeed(-1f, 1f), 0, GetRandomFromSeed(-1f, 1f));
                    newPos.x = Mathf.Min(8, Mathf.Max(-8, newPos.x));
                    Instantiate(experiencePrefab, newPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                }

                for (int i = 0; i < bombAtKill; i++)
                {
                    Vector3 newPos = transform.position + new Vector3(GetRandomFromSeed(-1f, 1f), 0, GetRandomFromSeed(-1f, 1f));
                    newPos.x = Mathf.Min(8, Mathf.Max(-8, newPos.x));
                    Instantiate(bombPrefab, newPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                Destroy(gameObject);
                gameManager.playerStats.AddScore(scoreAtKill);
            }
        }
    }

    private float GetRandomFromSeed(float min, float max)
    {
        float ret = gameManager.randomList[index] == 0 ? min : (max - min) / gameManager.randomList.Count * gameManager.randomList[index] + min;
        index = gameManager.randomList.Count > index + 1 ? index + 1 : 0;
        return ret;
    }

    protected Color NewColor(Color baseColor)
    {
        return baseColor + ((new Color(1, 1, 1) - baseColor) / startingHitpoints * (startingHitpoints - hitpoints));
    }
}
