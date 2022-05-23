using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = 3f;
        speed = 1f;
        scoreAtKill = 300;
        xpAtKill = 2;
        bombAtKill = 2;
        shootInterval = 0.75f;
        rotation = new Vector3(0, 0.25f, 0);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
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
}
