using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBehavior : ProjectileGeneral
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 6f;
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
            Destroy(other.gameObject.transform.parent.gameObject);
            gameManager.GameOver();
            // TODO: lives
        }
    }
}
