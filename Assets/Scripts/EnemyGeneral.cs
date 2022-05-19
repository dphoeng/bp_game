using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    protected float hitpoints;
    protected float speed;
    protected float shootInterval;
    protected int scoreAtKill;
    protected float delay = 0.0f;
    protected Vector3 rotation;
    protected GameObject experiencePrefab;
    protected GameObject projectilePrefab;
    protected GameManager gameManager;

    // Start is called before the first frame update, after child object's start
    protected virtual void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Rotate(rotation);
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
            Destroy(other.gameObject.transform.parent.gameObject);
            gameManager.GameOver();
        }
    }

    public float getHitpoints()
    {
        return hitpoints;
    }

    public void takeDamage(float damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Instantiate(experiencePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Destroy(gameObject);
            gameManager.AddScore(scoreAtKill);
        }
    }
}
