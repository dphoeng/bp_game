using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile1 : ProjectileGeneral
{
    //private PlayerController player;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 8f;
        // deals 1 damage from lvl 1-13 and 0.1 extra per level afterwards
        damage = Mathf.Max((GameObject.Find("Player").GetComponent<PlayerStats>().Level - 13) / 10, 0) + 1f;
        //player = GameObject.Find("Player").GetComponent<PlayerController>();
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
