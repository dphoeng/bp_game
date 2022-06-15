using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile3 : ProjectileGeneral
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 8f;
        // deals 0.5 damage from lvl 1-13 and 0.05 extra per level afterwards
        damage = Mathf.Max((GameObject.Find("Player").GetComponent<PlayerStats>().Level - 13) / 20, 0) + 0.5f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
