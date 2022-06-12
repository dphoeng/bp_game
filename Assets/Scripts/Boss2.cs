using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boss2 : EnemyGeneral
{
    public GameObject assignedProjectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;
    private GameObject player;
    public TextMeshProUGUI bossText;
    public Image maskBoss;
    public Image fillBoss;
    public bool activated = false;
    private bool lockedScreen = false;
    private bool noEnemies = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = startingHitpoints = 10;
        scoreAtKill = 2000;
        xpAtKill = 12;
        bombAtKill = 10;
        shootInterval = 0.5f;
        projectilePrefab = assignedProjectilePrefab;
        bombPrefab = assignedBombPrefab;
        experiencePrefab = assignedExperiencePrefab;
        player = GameObject.Find("Player");
        
        base.Start();

        bossText = spawnManager.bossText;
        maskBoss = spawnManager.maskBoss;
        fillBoss = spawnManager.fillBoss;
        maskBoss.fillAmount = hitpoints / startingHitpoints;
        bossText.text = "Ror'Mir";
        bossText.color = new Color(0, 1, 1);
        fillBoss.GetComponent<Image>().color = new Color(0, 1, 1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!noEnemies)
        {
            if (!GameObject.FindWithTag("Enemy Projectile") && !GameObject.FindWithTag("Enemy"))
            {
                spawnManager.blinderScreen.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
                spawnManager.blinderScreen.SetActive(true);
                noEnemies = true;
            }
        } else
        {
            if (!activated && !lockedScreen)
            {
                if (spawnManager.blinderScreen.GetComponent<Renderer>().material.color.a >= 1)
                {
                    lockedScreen = true;
                    player.GetComponent<PlayerController>().lockMovement = true;
                    player.transform.SetPositionAndRotation(new Vector3(0, 5, -15), Quaternion.Euler(0, 0, 0));
                    Invoke(nameof(ActivateBoss), 1);
                } else
                {
                    spawnManager.blinderScreen.GetComponent<Renderer>().material.color += new Color(0, 0, 0, 0.333f) * Time.deltaTime;
                }

            }
            if (delay <= Time.time && activated)
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
                delay = Time.time + shootInterval;
            }
        }
    }

    private void ActivateBoss()
    {
        activated = true;
        lockedScreen = false;
        spawnManager.blinderScreen.SetActive(false);
        transform.parent.position = new Vector3(0, 5, -5);
        player.GetComponent<PlayerController>().lockMovement = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.playerStats.LoseLive();
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().touchedBoss = true;
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().touchedBossTime = 0.5f + Time.time;
            transform.parent.GetComponent<Boss2Movement>().touchedPlayer = true;
            transform.parent.GetComponent<Boss2Movement>().touchedPlayerTime = 0.5f + Time.time;
            if (gameManager.playerStats.Lives < 0)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
        base.OnTriggerEnter(other);
        maskBoss.fillAmount = hitpoints / startingHitpoints;
    }
}
