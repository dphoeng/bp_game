using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;

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
        hitpoints = 1f + (1f * (spawnManager.timePast - 45) / 200);
        speed = Mathf.Max(0.2f, 1f - (spawnManager.timePast / 500));
        shootInterval = 1f;
        // Debug.Log("Enemy3 spawned at " + spawnManager.timePast + " has shootInterval: " + shootInterval + ", hitpoints: " + hitpoints + " and speed: " + speed);
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
}
