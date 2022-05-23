using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileBehavior : ProjectileGeneral
{
    private PlayerController player;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 8f;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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
            other.transform.root.gameObject.GetComponent<EnemyGeneral>().takeDamage(1, Time.frameCount);
        }
    }
}
