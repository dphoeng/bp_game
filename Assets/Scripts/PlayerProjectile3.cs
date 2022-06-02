using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile3 : ProjectileGeneral
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 8f;
        damage = 0.5f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
