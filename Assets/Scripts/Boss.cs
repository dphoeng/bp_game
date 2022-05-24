using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = 100f;
        speed = 1f;
        scoreAtKill = 2000;
        xpAtKill = 12;
        bombAtKill = 10;
        shootInterval = 1f;
        rotation = new Vector3(0, 0.5f, 0);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //if (delay <= Time.time && transform.position.z < 0)
        //{
        //    Instantiate(projectilePrefab, transform.position, transform.rotation);
        //    delay = Time.time + shootInterval;
        //}
        base.Update();
    }
}
