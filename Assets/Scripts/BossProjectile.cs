using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : ProjectileGeneral
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 5f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
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
}
