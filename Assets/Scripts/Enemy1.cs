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
        hitpoints = 2f;
        speed = 2f;
        scoreAtKill = 100;
        xpAtKill = 1;
        bombAtKill = 1;
        shootInterval = 0.5f;
        rotation = new Vector3(0, 0, 0.5f);
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
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            delay = Time.time + shootInterval;
        }
        base.Update();
    }
}
