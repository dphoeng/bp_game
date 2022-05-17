using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileBehavior : ProjectileGeneral
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 8f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.root.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.transform.root.gameObject.GetComponent<EnemyGeneral>().takeDamage(1);
            // TODO: Score count
        }
    }
}
