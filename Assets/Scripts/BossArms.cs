using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArms : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;
    private Color startColor;
    private bool isQuitting;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = 10f;
        speed = 0f;
        isQuitting = false;
        scoreAtKill = 200;
        xpAtKill = 2;
        bombAtKill = 0;
        shootInterval = 0.2f;
        rotation = new Vector3(0, 0, 0);
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        startColor = transform.GetComponent<Renderer>().material.color;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.GetComponent<Renderer>().material.color = startColor + ((new Color(1, 1, 1) - startColor) / 10 * (10 - hitpoints));
        transform.LookAt(transform.parent.transform);
        if (delay <= Time.time && transform.parent.transform.position.z <= -5)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Renderer>().material.SetColor("_Color", transform.GetComponent<Renderer>().material.color);
            delay = Time.time + shootInterval;
        }
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
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
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting && GameObject.Find("Player"))
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 45 * i, 0)));
                projectile.GetComponent<Renderer>().material.SetColor("_Color", transform.GetComponent<Renderer>().material.color);
            }
        }
    }
}
