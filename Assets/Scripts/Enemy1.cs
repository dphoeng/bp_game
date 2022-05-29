using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        scoreAtKill = 100;
        xpAtKill = 1;
        bombAtKill = 1;
        rotation = new Vector3(0, 0, 0.5f);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
        speed = 2f;
        shootInterval = Mathf.Max(0.4f, 0.5f - (spawnManager.timePast / 6000));
        hitpoints = 2f + 1f * (spawnManager.timePast - 20) / 100;
        Debug.Log("Enemy1 spawned at " + spawnManager.timePast + " has shootInterval: " + shootInterval + ", hitpoints: " + hitpoints + " and speed: " + speed);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (delay <= Time.time && transform.position.z < 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            delay = Time.time + shootInterval;
        }
        base.Update();
    }
}
