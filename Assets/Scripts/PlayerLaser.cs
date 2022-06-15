using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : ProjectileGeneral
{
    private Material laserMaterial;

    // Start is called before the first frame update
    protected override void Start()
    {
        laserMaterial = gameObject.GetComponent<Renderer>().material;
        laserMaterial.shader = Shader.Find("Transparent/Diffuse");
        transform.position += transform.forward * 15f;
        // deals 1.5 damage from lvl 7-13 and 0.16667 extra per level afterwards
        damage = Mathf.Max((GameObject.Find("Player").GetComponent<PlayerStats>().Level - 13) / 6, 0) + 1.5f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (laserMaterial.color.a > 0)
            laserMaterial.color -= new Color(0, 0, 0, Time.deltaTime);
        else
            Destroy(gameObject);
    }
}
