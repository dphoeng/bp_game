using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Movement : MonoBehaviour
{
    private Boss2 childObj;
    private GameObject player;
    public float touchedPlayerTime;
    public bool touchedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        childObj = transform.GetChild(0).GetComponent<Boss2>();
        player = GameObject.Find("Player");
        touchedPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.rotation = player.transform.rotation * Quaternion.Euler(new Vector3(0, 0, 180));
        }
        if (transform.childCount < 1)
        {
            Destroy(gameObject);
        }
        if (childObj.activated && player)
        {
            transform.Translate(player.GetComponent<PlayerController>().speed * Time.deltaTime * new Vector3(Input.GetAxisRaw("Horizontal") * -1, 0, Input.GetAxisRaw("Vertical") * -1), Space.World);

            // Border control
            if (transform.position.z < -17.5f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -17.5f);
            if (transform.position.z > -2.5f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
            if (transform.position.x > 7.5f)
                transform.position = new Vector3(7.5f, transform.position.y, transform.position.z);
            if (transform.position.x < -7.5f)
                transform.position = new Vector3(-7.5f, transform.position.y, transform.position.z);

            if (touchedPlayer)
            {
                if (touchedPlayerTime > Time.time)
                {
                    if (player)
                        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, player.GetComponent<PlayerController>().speed * Time.deltaTime * -1 * 2);
                }
                else
                    touchedPlayer = false;
            }
        }

    }
}
