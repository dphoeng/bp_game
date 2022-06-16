using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileMain : ProjectileGeneral
{
    private Renderer projectileRenderer;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 8f;
        projectileRenderer = transform.GetComponent<Renderer>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        projectileRenderer.material.color = new Color(Wave(0), Wave(2), Wave(4));
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.playerStats.LoseLive();
            if (gameManager.playerStats.Lives < 0)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }

    private float Wave(int add)
    {
        return 0.5f * Mathf.Sin(2 * Mathf.PI / 1.5f * Time.time + add) + 0.5f;
    }
}
