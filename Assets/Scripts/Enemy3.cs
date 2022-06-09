using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;
    private Color materialColor = new Color(0, 0.2f, 0);
    private Color armsMaterialColor = new Color(0.3f, 0.6f, 0.3f);

    // Start is called before the first frame update
    protected override void Start()
    {
        scoreAtKill = 200;
        xpAtKill = 1;
        bombAtKill = 1;
        rotation = new Vector3(0, 0, 0);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
        startingHitpoints = hitpoints = 0.8f + (1f * (spawnManager.timePast - 30) / 200);
        speed = Mathf.Max(0.2f, 1f - (spawnManager.timePast / 500));
        shootInterval = 1f;
        // Debug.Log("Enemy3 spawned at " + spawnManager.timePast + " has shootInterval: " + shootInterval + ", hitpoints: " + hitpoints + " and speed: " + speed);
        transform.GetComponent<Renderer>().material.color = materialColor;
        transform.GetChild(0).GetComponent<Renderer>().material.color = armsMaterialColor;
        transform.GetChild(1).GetComponent<Renderer>().material.color = armsMaterialColor;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (gameManager.player)
        {
            transform.LookAt(gameManager.player.gameObject.transform);
            transform.rotation = transform.rotation * Quaternion.Euler(0, 180f, 0);
        }
        if (delay <= Time.time && transform.position.z < 0)
        {
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
        if (other.transform.CompareTag("Player Projectile") || other.transform.CompareTag("Player Laser"))
        {
            transform.GetComponent<Renderer>().material.color = NewColor(materialColor);
            transform.GetChild(0).GetComponent<Renderer>().material.color = NewColor(armsMaterialColor);
            transform.GetChild(1).GetComponent<Renderer>().material.color = NewColor(armsMaterialColor);
        }
    }
}
