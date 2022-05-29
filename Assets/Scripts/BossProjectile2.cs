using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile2 : ProjectileGeneral
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 5f;
        if (GameObject.Find("Player"))
        {
            transform.LookAt(GameObject.Find("Player").transform);
            transform.Rotate(new Vector3(0, 180, 0));
        }
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (speed > 2.5f)
        {
            speed -= 0.005f;
        }
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
