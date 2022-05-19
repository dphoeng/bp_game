using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfDeathBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(0.05f, 0, 0.05f);
        if (transform.localScale.x > 30)
		{
            Destroy(gameObject);
		}
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.transform.root.CompareTag("Enemy") || other.transform.root.CompareTag("Enemy Projectile")) && other.transform.position.z < 0)
        {
            Destroy(other.transform.root.gameObject);
        }
    }
}
