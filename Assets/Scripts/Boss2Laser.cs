using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Laser : MonoBehaviour
{
    private Material laserMaterialChild;
    private Material laserMaterial;
    private bool deactivate = false;

    // Start is called before the first frame update
    void Start()
    {
        laserMaterial = gameObject.GetComponent<Renderer>().material;
        laserMaterialChild = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        laserMaterial.shader = Shader.Find("Transparent/Diffuse");
        laserMaterialChild.shader = Shader.Find("Transparent/Diffuse");
        Invoke(nameof(ActivateLaser), 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (deactivate)
        {
            if (laserMaterialChild.color.a > 0)
            {
                laserMaterialChild.color -= new Color(0, 0, 0, Time.deltaTime);
            }
            else
                Destroy(gameObject);
        }

    }

    private void ActivateLaser()
    {
        if (!GameObject.FindGameObjectWithTag("Ring of Death"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
            deactivate = true;
        } else
            Destroy(gameObject);
    }
}
