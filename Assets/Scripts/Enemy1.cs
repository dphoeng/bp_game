using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = 2f;
        speed = 2f;
        shootInterval = 0.75f;
        rotation = new Vector3(0, 0, 0.5f);
        projectilePrefab = assignedProjectilePrefab;
        base.Start();
    }
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
