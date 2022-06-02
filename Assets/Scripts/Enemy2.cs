using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;
    private Color materialColor = new Color(0, 0, 1f);
    private Color armsMaterialColor = new Color(0.3f, 0.3f, 1f);

    // Start is called before the first frame update
    protected override void Start()
    {
        // at 60 seconds (possible first spawn) has 3hp, only at 90s or more has more than 3 hp
        scoreAtKill = 300;
        xpAtKill = 2;
        bombAtKill = 2;
        rotation = new Vector3(0, 0.25f, 0);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
        startingHitpoints = hitpoints = 3f + 3f * (spawnManager.timePast - 90) / 100;
        speed = 1f;
        shootInterval = 0.75f;
        // Debug.Log("Enemy2 spawned at " + spawnManager.timePast + " has shootInterval: " + shootInterval + ", hitpoints: " + hitpoints + " and speed: " + speed);
        transform.GetComponent<Renderer>().material.color = materialColor;
        transform.GetChild(0).GetComponent<Renderer>().material.color = armsMaterialColor;
        transform.GetChild(1).GetComponent<Renderer>().material.color = armsMaterialColor;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (delay <= Time.time && transform.position.z < 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180f, 0));
            Instantiate(projectilePrefab, transform.position, transform.rotation);
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
        if (other.transform.CompareTag("Player Projectile"))
        {
            transform.GetComponent<Renderer>().material.color = NewColor(materialColor);
            transform.GetChild(0).GetComponent<Renderer>().material.color = NewColor(armsMaterialColor);
            transform.GetChild(1).GetComponent<Renderer>().material.color = NewColor(armsMaterialColor);
        }
    }
}
