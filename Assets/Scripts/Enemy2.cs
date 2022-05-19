using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = 4f;
        speed = 1f;
        scoreAtKill = 100;
        shootInterval = 0.75f;
        rotation = new Vector3(0, 0.25f, 0);
        projectilePrefab = assignedProjectilePrefab;
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
