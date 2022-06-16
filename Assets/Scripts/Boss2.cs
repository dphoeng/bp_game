using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boss2 : EnemyGeneral
{
    public List<GameObject> projectilePrefab;
    public GameObject assignedExperiencePrefab;
    public GameObject assignedBombPrefab;
    private GameObject lastProjectile;
    private GameObject player;
    public TextMeshProUGUI bossText;
    public Image maskBoss;
    public Image fillBoss;
    private float initialLaserDelay;
    private float shootInterval2;
    private float delay2 = 0f;
    private float shootInterval3;
    private float delay3 = 0f;
    private float regenLaserInterval = 0.075f;
    private float regenLaserDelay = 0f;
    private int indexLaser = 0;
    private int indexPhaseEnd = 0;
    public bool activated = false;
    public bool secondPhaseActivated = false;
    private bool lockedScreen = false;
    private bool noEnemies = false;
    private bool secondPhase = false;
    public bool shieldPhaseLaserDelay = false;
    public float shieldPhaseShootDuration = 3f;
    public float shieldPhaseShootInterval = 0.02f;
    public float shieldPhaseShootDelay = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        hitpoints = startingHitpoints = 50;
        scoreAtKill = 4000;
        xpAtKill = 20;
        bombAtKill = 15;
        shootInterval = 0.3f;
        shootInterval2 = 0.15f;
        shootInterval3 = 1.3f;
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
            if (!activated && !lockedScreen && !secondPhase && !secondPhaseActivated)
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
            if (activated)
            {
                if (!secondPhase)
                {
                    if (!GameObject.FindGameObjectWithTag("Ring of Death"))
                    {
                        if (delay <= Time.time)
                        {
                            if (hitpoints < 45)
                            {
                                lastProjectile = Instantiate(projectilePrefab[0], transform.position, transform.rotation);
                                lastProjectile.transform.Translate(new Vector3(0.3f, 0, 0), Space.Self);
                                lastProjectile = Instantiate(projectilePrefab[0], transform.position, transform.rotation);
                                lastProjectile.transform.Translate(new Vector3(-0.3f, 0, 0), Space.Self);
                            } else
                                lastProjectile = Instantiate(projectilePrefab[0], transform.position, transform.rotation);
                            if (hitpoints < 30)
                            {
                                lastProjectile = Instantiate(projectilePrefab[2], transform.position, transform.rotation * Quaternion.Euler(0, -25f, 0));
                                lastProjectile.transform.Translate(new Vector3(0.3f, 0, 0), Space.Self);
                                lastProjectile = Instantiate(projectilePrefab[2], transform.position, transform.rotation * Quaternion.Euler(0, 25f, 0));
                                lastProjectile.transform.Translate(new Vector3(-0.3f, 0, 0), Space.Self);
                            }    
                            delay = Time.time + shootInterval;
                        }
                        if (delay2 <= Time.time && hitpoints < 25)
                        {
                            Instantiate(projectilePrefab[1], transform.position, transform.rotation);
                            delay2 = Time.time + shootInterval2;
                        }
                        if (delay3 <= Time.time && initialLaserDelay <= Time.time && hitpoints < 37)
                        {
                            lastProjectile = Instantiate(projectilePrefab[3], transform.position, transform.rotation * Quaternion.Euler(0, 0, 180));
                            lastProjectile.transform.position -= transform.forward * 28f;
                            delay3 = Time.time + shootInterval3;
                        }
                    }
                }
            }
            if (secondPhase)
            {
                if (hitpoints < 75)
                {
                    hitpoints += Time.deltaTime * 3f;
                    maskBoss.fillAmount = hitpoints / startingHitpoints;
                    if (regenLaserDelay <= Time.time && !GameObject.FindGameObjectWithTag("Ring of Death") && shieldPhaseLaserDelay)
                    {
                        Instantiate(projectilePrefab[3], new Vector3(GetRandomFromSeedLaser(-7.5f, 7.5f), 5, GetRandomFromSeedLaser(-17.5f, -2.5f)), transform.rotation * Quaternion.Euler(0, GetRandomFromSeedLaser(0, 180), 180));
                        regenLaserDelay = Time.time + regenLaserInterval;
                    }
                }
                else
                {
                    secondPhase = false;
                    secondPhaseActivated = true;
                    transform.root.GetChild(1).gameObject.SetActive(false);
                    hitpoints = 75;
                    bossText.text = "Ror'Mir (Unleashed)";
                }
            }
            if (shieldPhaseShootDuration >= Time.time)
			{
                if (shieldPhaseShootDelay <= Time.time && !GameObject.FindGameObjectWithTag("Ring of Death"))
				{
                    for (int x = 0; x < 6; x++)
					{
                        float res = GetRandomFromSeed(0, 6.5f);
                        int ind = Mathf.FloorToInt(res / 2);

                        if (ind == 3)
						{
                            lastProjectile = Instantiate(projectilePrefab[ind], transform.position, transform.rotation * Quaternion.Euler(0, GetRandomFromSeed(0, 360), 180));
                            lastProjectile.transform.position -= lastProjectile.transform.forward * 28f;
                        }
                        else
						{
                            Instantiate(projectilePrefab[ind], transform.position, transform.rotation * Quaternion.Euler(0, GetRandomFromSeed(0, 360), 0));
                        }
                    }
                    shieldPhaseShootDelay = Time.time + shieldPhaseShootInterval;
                }
			}
        }
    }

    private void ActivateShieldPhaseShoot()
	{
        shieldPhaseLaserDelay = true;
	}

    private void ActivateBoss()
    {
        activated = true;
        lockedScreen = false;
        spawnManager.blinderScreen.SetActive(false);
        initialLaserDelay = Time.time + 1f;
        transform.parent.position = new Vector3(0, 5, -5);
        player.GetComponent<PlayerController>().lockMovement = false;
    }

    private float GetRandomFromSeedLaser(float min, float max)
    {
        float ret = gameManager.randomList[indexLaser] == 0 ? min : (max - min) / gameManager.randomList.Count * gameManager.randomList[indexLaser] + min;
        indexLaser = gameManager.randomList.Count > indexLaser + 1 ? indexLaser + 1 : 0;
        return ret;
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
        else if (other.transform.CompareTag("Player Projectile"))
        {
            if (!secondPhase)
            {
                TakeDamageSpecial(other.GetComponent<ProjectileGeneral>().damage, Time.frameCount);
            }
            Destroy(other.gameObject);
        }
        // if hit by player laser, check if current position is inside field, check whether this object has already been hit by this laser already and check whether the alpha value of the laser is higher than 0.95 (fresh laser)
        else if (other.transform.CompareTag("Player Laser") && transform.position.z < 0 && lastLaserHit != other.gameObject.GetInstanceID() && other.gameObject.GetComponent<Renderer>().material.color.a > 0.95f)
        {
            if (!secondPhase)
                TakeDamageSpecial(other.GetComponent<ProjectileGeneral>().damage, Time.frameCount);
        }
        maskBoss.fillAmount = hitpoints / startingHitpoints;
    }

    public void TakeDamageSpecial(float damage, int frame)
    {
        // taking damage for first phase
        if (!secondPhase && !secondPhaseActivated)
        {
            if (frame != lastFrame)
            {
                lastFrame = frame;
                hitpoints -= damage;
                if (hitpoints <= 0)
                {
                    startingHitpoints = 75;
                    hitpoints = 0;
                    transform.root.GetChild(1).gameObject.SetActive(true);
                    activated = false;
                    secondPhase = true;
                    bossText.text = "Ror'Mir (Recovering)";
                    Invoke(nameof(ActivateShieldPhaseShoot), 5);
                    shieldPhaseShootDuration += Time.time;
                }
            }
        }

        // taking damage in second phase, this prevents the object from being destroyed prematurely
        else if (secondPhaseActivated)
        {
            TakeDamage(damage, frame);
        }
    }

    private float GetRandomFromSeed(float min, float max)
    {
        float ret = gameManager.randomList[indexPhaseEnd] == 0 ? min : (max - min) / gameManager.randomList.Count * gameManager.randomList[indexPhaseEnd] + min;
        indexPhaseEnd = gameManager.randomList.Count > indexPhaseEnd + 1 ? indexPhaseEnd + 1 : 0;
        return ret;
    }
}
