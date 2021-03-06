using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile2 : ProjectileGeneral
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 6f;
        // for every level gain 0.25 damage
        damage = (GameObject.Find("Player").GetComponent<PlayerStats>().Level - 13) / 4 + 2;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    //  private void OnTriggerEnter(Collider other)
    //  {

    //      if (other.transform.root.CompareTag("Enemy"))
    //      {
    //          Destroy(gameObject);
    //          other.transform.root.gameObject.GetComponent<EnemyGeneral>().takeDamage(1, Time.frameCount);
    //      } else if (other.transform.CompareTag("Boss") || other.transform.CompareTag("Boss Arms"))
    //{
    //          Destroy(gameObject);
    //          other.transform.gameObject.GetComponent<EnemyGeneral>().takeDamage(1, Time.frameCount);
    //      }
    //  }
}
