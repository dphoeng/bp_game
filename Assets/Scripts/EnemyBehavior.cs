using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float hitpoints;
    private float speed;
    private float shootInterval;
    private float delay = 0.0f;
    private Vector3 rotation;
    private GameObject projectilePrefab;
    public List<GameObject> projectilePrefabList;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Enemy1"))
        {
            hitpoints = 2f;
            speed = 2f;
            shootInterval = 0.75f;
            rotation = new Vector3(0, 0, 0.5f);
            projectilePrefab = projectilePrefabList[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation);
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < -25)
        {
            Destroy(gameObject);
        }

        if (delay <= Time.time)
        {
            Debug.Log(transform.position);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            delay = Time.time + shootInterval;
        }
    }

    public float getHP()
    {
        return hitpoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

    public void takeDamage(float damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
