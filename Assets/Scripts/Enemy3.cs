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
        hitpoints = 1f;
        speed = 1f;
        scoreAtKill = 200;
        xpAtKill = 1;
        bombAtKill = 1;
        shootInterval = 1f;
        rotation = new Vector3(0, 0, 0);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
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
