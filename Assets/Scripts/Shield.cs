using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : EnemyGeneral
{
    private Renderer shieldRenderer;
    public GameObject projectilePrefab;
    private GameObject lastProjectile;
    private Boss2 boss2;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = startingHitpoints = 9999999;
        scoreAtKill = 99999;
        xpAtKill = 0;
        bombAtKill = 0;
        shootInterval = 0.3f;
        rotation = new Vector3(0, 0.5f, 0);
        speed = 0;
        boss2 = transform.parent.GetChild(0).GetComponent<Boss2>();
        base.Start();
        shieldRenderer = gameObject.GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!boss2.explode)
        {
            shieldRenderer.material.color += new Color(Time.deltaTime * 0.04f, Time.deltaTime * 0.04f, Time.deltaTime * 0.04f);
            if (delay <= Time.time && !GameObject.FindGameObjectWithTag("Ring of Death") && boss2.shieldPhaseLaserDelay)
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
        } else
        {
            transform.localScale += new Vector3(Time.deltaTime * 20, 0, Time.deltaTime * 20);
        }
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!boss2.explode)
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
        else if (other.transform.root.CompareTag("Enemy Projectile"))
        {
            Destroy(other.transform.root.gameObject);
        }
    }
}
