using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : EnemyGeneral
{
    private Renderer shieldRenderer;
    public GameObject projectilePrefab;
    private GameObject lastProjectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = startingHitpoints = 9999999;
        scoreAtKill = 99999;
        xpAtKill = 0;
        bombAtKill = 0;
        shootInterval = 0.2f;
        rotation = new Vector3(0, 0, 0);
        rotation = new Vector3(0, 0.5f, 0);
        speed = 0;
        base.Start();
        shieldRenderer = gameObject.GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    protected override void Update()
    {
        shieldRenderer.material.color += new Color(Time.deltaTime * 0.1f, Time.deltaTime * 0.1f, Time.deltaTime * 0.1f);
        if (delay <= Time.time && !GameObject.FindGameObjectWithTag("Ring of Death"))
        {
            for (int x = 0; x < 4; x++)
            {
                lastProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, -90 + x * 90, 0));
                lastProjectile.transform.Translate(new Vector3(0, 0, -1.1f), Space.Self);
                if (GameObject.Find("Player"))
                {
                    lastProjectile.transform.LookAt(GameObject.Find("Player").transform);
                    lastProjectile.transform.Rotate(new Vector3(0, 180, 0));
                }
            }
            delay = shootInterval + Time.time;
        }
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.playerStats.LoseLive();
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().touchedBoss = true;
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().touchedBossTime = 0.5f + Time.time;
            if (gameManager.playerStats.Lives < 0)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
        base.OnTriggerEnter(other);
    }
}
