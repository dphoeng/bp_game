using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : EnemyGeneral
{
    public GameObject projectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;
    private bool shot = false;
    private Color materialColor = new Color(0.7f, 0.7f, 0.7f);

    // Start is called before the first frame update
    protected override void Start()
    {
        // at 60 seconds (possible first spawn) has 3hp, only at 90s or more has more than 3 hp
        scoreAtKill = 400;
        xpAtKill = 3;
        bombAtKill = 2;
        rotation = new Vector3(0, 0.05f, 0);
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
        startingHitpoints = hitpoints = 5f + 4f * (spawnManager.timePast - 160) / 100;
        speed = 0.7f;
        shootInterval = Mathf.Max(0.7f, 1f - ((spawnManager.timePast - 160) / 1000));
        //Debug.Log("Enemy4 spawned at " + spawnManager.timePast + " has shootInterval: " + shootInterval + ", hitpoints: " + hitpoints + " and speed: " + speed);

        transform.GetComponent<Renderer>().material.color = materialColor;

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (delay <= Time.time && transform.position.z < 0 && !GameObject.FindGameObjectWithTag("Ring of Death"))
        {
            if (shot)
            {
                transform.GetComponent<Animator>().Play("Shoot1");
                Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180f, 0));
                Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 270f, 0));
                Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 90f, 0));
                Instantiate(projectilePrefab, transform.position, transform.rotation);
			}
            else
			{
                transform.GetComponent<Animator>().Play("Shoot2");
                Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 315f, 0));
                Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 225f, 0));
                Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 135f, 0));
                Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 45f, 0));
            }
            shot = !shot;
            delay = Time.time + shootInterval;
        }
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
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
        base.OnTriggerEnter(other);
        if (other.transform.CompareTag("Player Projectile") || other.transform.CompareTag("Player Laser"))
        {
            transform.GetComponent<Renderer>().material.color = NewColor(materialColor);
        }
    }
}
