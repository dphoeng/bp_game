using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float laserDelay;
    private bool laserMarkerActive;
    private GameObject laserChild;

    // Start is called before the first frame update
    void Start()
    {
        laserDelay = 0.5f + Time.time;
        laserChild = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") && laserDelay >= Time.time)
        {
            transform.position = GameObject.Find("Player").transform.position;
            transform.Rotate(new Vector3(0, 0.1f, 0) * Time.deltaTime * 700);
            transform.DetachChildren();
            if (transform.localScale.z > 0.2f)
                transform.localScale += new Vector3(0, 0, -0.06f) * Time.deltaTime * 300;
            laserChild.transform.parent = transform;
        }
        else if (laserDelay < Time.time && !laserMarkerActive)
        {
            laserMarkerActive = true;
        }
        if (laserDelay + 0.5f < Time.time && laserMarkerActive)
        {
            laserChild.SetActive(true);
            laserChild.transform.Translate(Vector3.forward * 150f * Time.deltaTime);
            if (laserDelay + 1f < Time.time)
            {
                laserMarkerActive = false;
                Destroy(gameObject);
            }
        }
    }
}
