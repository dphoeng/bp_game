using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boss : EnemyGeneral
{
    public GameObject projectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;
    public GameObject targetMarkerPrefab;
    public GameObject projectileMainPrefab;
    public TextMeshProUGUI bossText;
    public Image maskBoss;
    public Image fillBoss;
    private Renderer bossRenderer;
    private float moveInterval;
    private float moveDelay;
    private float moveSpeed;
    private List<Vector3> locationList;
    private Vector3 newPos;
    private bool newPosMove;
    private float delayMain = 0f;
    private float intervalMain = 0.2f;
    private float delay2 = 0f;
    private float interval2 = 0.1f;
    private float breakInterval = 0f;
    private float breakIntervalMain = 0f;
    private bool inBreak = true;
    private bool inBreakMain = true;
    private int indexMove;
    private bool noEnemies = false;


    // Start is called before the first frame update
    protected override void Start()
    {
        ColorChildren();
        hitpoints = startingHitpoints = 50f;
        moveInterval = 4f;
        moveDelay = 0f;
        speed = 1.5f;
        moveSpeed = 2f;
        scoreAtKill = 2000;
        xpAtKill = 12;
        bombAtKill = 10;
        shootInterval = 7.5f;
        newPosMove = false;
        locationList = CreateNewList();
        newPos = locationList[Random.Range(0, 8)];
        rotation = new Vector3(0, 0.5f, 0);
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        bossRenderer = GetComponent<Renderer>();
        base.Start();

        bossText = spawnManager.bossText;
        maskBoss = spawnManager.maskBoss;
        fillBoss = spawnManager.fillBoss;
        bossText.text = "Big Boss Luigi";
        maskBoss.fillAmount = hitpoints / startingHitpoints;
        bossText.color = new Color(Wave(1.5f, Time.time, 0), Wave(1.5f, Time.time, 2), Wave(1.5f, Time.time, 4));
        fillBoss.GetComponent<Image>().color = new Color(Wave(1.5f, Time.time, 0), Wave(1.5f, Time.time, 2), Wave(1.5f, Time.time, 4));
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!noEnemies)
        {
            if (!GameObject.FindWithTag("Enemy Projectile") && !GameObject.FindWithTag("Enemy"))
            {
                noEnemies = true;
            }
        }
        else
        {
            if (transform.position.z > -5)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
            else
            {
                speed = 0f;
                if (moveDelay < Time.time)
                {
                    int res = Mathf.FloorToInt(gameManager.randomList[indexMove] / (1000 / (locationList.Count)));
                    indexMove = gameManager.randomList.Count > indexMove + 1 ? indexMove + 1 : 0;
                    newPos = locationList[res];
                    locationList.RemoveAt(res);
                    newPosMove = true;
                    moveDelay = Time.time + 9999f;
                }
                if (newPosMove && transform.position != newPos)
                {
                    transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
                }
                else if (newPosMove)
                {
                    newPosMove = false;
                    moveDelay = Time.time + moveInterval;
                    if (locationList.Count == 0)
                    {
                        locationList = CreateNewList();
                        locationList.Remove(newPos);
                    }
                }
                if (!GameObject.FindGameObjectWithTag("Ring of Death"))
                {
                    if (transform.childCount < 7)
                    {
                        if (delay <= Time.time)
                        {
                            if (GameObject.Find("Player"))
                            {
                                Instantiate(targetMarkerPrefab, GameObject.Find("Player").transform.position, transform.rotation);
                                Instantiate(targetMarkerPrefab, GameObject.Find("Player").transform.position, transform.rotation * Quaternion.Euler(0, 90f, 0));
                                delay = Time.time + shootInterval;
                            }
                        }
                    }
                    if (transform.childCount < 4)
                    {
                        if (delay2 < Time.time && !inBreak)
                        {
                            GameObject shot = Instantiate(projectilePrefab, transform.position, transform.rotation);
                            shot.GetComponent<Renderer>().material.color = bossRenderer.material.color;
                            delay2 = Time.time + interval2;
                        }
                    }

                    if (delayMain < Time.time && !inBreakMain)
                    {
                        Instantiate(projectileMainPrefab, transform.position, transform.rotation);
                        Instantiate(projectileMainPrefab, transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
                        Instantiate(projectileMainPrefab, transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 180, 0)));
                        Instantiate(projectileMainPrefab, transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 270, 0)));
                        delayMain = Time.time + intervalMain;
                    }
                }
                if (transform.childCount < 4)
                {
                    moveInterval = 2;
                    if (breakInterval < Time.time)
                    {
                        inBreak = !inBreak;
                        breakInterval = inBreak ? Time.time + 0.5f : Time.time + 2f;
                    }
                }
                if (transform.childCount < 1)
                {
                    moveInterval = 0;
                    shootInterval = 3f;
                    intervalMain = 0.15f;
                }
                if (breakIntervalMain < Time.time)
                {
                    inBreakMain = !inBreakMain;
                    breakIntervalMain = inBreakMain ? Time.time + 1f : Time.time + 1f;
                }

            }
            base.Update();
        }
        bossRenderer.material.color = new Color(Wave(1.5f, Time.time, 0), Wave(1.5f, Time.time, 2), Wave(1.5f, Time.time, 4));
        bossText.color = new Color(Wave(1.5f, Time.time, 0), Wave(1.5f, Time.time, 2), Wave(1.5f, Time.time, 4));
        fillBoss.color = new Color(Wave(1.5f, Time.time, 0), Wave(1.5f, Time.time, 2), Wave(1.5f, Time.time, 4));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.playerStats.LoseLive();
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().touchedBoss = true;
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().touchedBossTime = 0.5f + Time.time;
            if (gameManager.playerStats.Lives < 0)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
        base.OnTriggerEnter(other);
        maskBoss.fillAmount = hitpoints / startingHitpoints;
    }

    private List<Vector3> CreateNewList()
    {
        return new List<Vector3>() { new Vector3(0, 5, -5), new Vector3(3.536f, 5, -6.464f), new Vector3(5, 5, -10), new Vector3(3.536f, 5, -13.536f), new Vector3(0, 5, -15), new Vector3(-3.536f, 5, -13.536f), new Vector3(-5, 5, -10), new Vector3(-3.536f, 5, -6.464f) };
    }

    private void ColorChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", new Color(Wave(8f, i, 0), Wave(8f, i, 2), Wave(8f, i, 4)));
        }
    }

    private float Wave(float lambda, float i, int add)
    {
        return 0.5f * Mathf.Sin(2 * Mathf.PI / lambda * i + add) + 0.5f;
    }
}
