using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundObjects : MonoBehaviour
{
    private float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < -25)
		{
            Destroy(gameObject);
		}
    }
}
