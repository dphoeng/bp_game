using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArms : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = 10f;
        speed = 0f;
        scoreAtKill = 200;
        xpAtKill = 2;
        bombAtKill = 0;
        shootInterval = 0.2f;
        rotation = new Vector3(0, 0, 0);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.LookAt(transform.parent.transform);
        if (delay <= Time.time && transform.position.z < 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            delay = Time.time + shootInterval;
        }
        base.Update();
    }
}
