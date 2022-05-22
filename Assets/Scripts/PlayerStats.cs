using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private GameManager gameManager;
    public int Score {get; set;}
    public float Experience { get; set; }
    public float TotalLevel { get; set; }
    public int Level { get; set; }
    public float BombProgress { get; set; }
    public int BombCount { get; set; }
    public int Lives { get; set; }
    public float Strength { get; set; }
    public float AttackSpeed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Score = 0;
        Experience = 0;
        TotalLevel = 0;
        Level = 1;
        BombProgress = 0;
        BombCount = 2;
        Lives = 2;
        Strength = 1;
        AttackSpeed = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        gameManager.scoreText.text = "Score<br>" + Score;
    }

    public void AddXp(float xpToAdd)
    {
        Experience += xpToAdd;
        if (Experience >= 100f)
        {
            Experience -= 100f;
            LevelUp();
        }
        gameManager.levelText.text = "Lvl. " + Level;
        gameManager.maskLevel.fillAmount = Experience / 100f;
    }

    private void LevelUp()
    {
        Level += 1;
        TotalLevel += 1;
        Strength += 0.1f;
        
        if (AttackSpeed > 0.1f)
            AttackSpeed -= 0.0075f;    

        // every 3 levels gain an extra life
        if ((TotalLevel - 1) % 3 == 0)
        {
            AddLives(1);
        }
        // TODO: New player projectile or stronger on level up
    }

    public void AddBombPrg(float vToAdd)
    {
        BombProgress += vToAdd;
        if (BombProgress >= 100f)
        {
            BombProgress -= 100f;
            AddBomb(1);
        }
        gameManager.bombText.text = BombCount + "";
        gameManager.maskBomb.fillAmount = BombProgress / 100f;
    }

    public void AddLives(int livesToAdd)
    {
        Lives += livesToAdd;
        if (livesToAdd > 0)
        {
            gameManager.livesList.Add(Instantiate(gameManager.livesPrefab, new Vector3(12.5f + 26.25f * (gameManager.livesList.Count % 5), -12.5f - 26.5f * (Mathf.FloorToInt(gameManager.livesList.Count / 5)), 0), transform.rotation));
            gameManager.livesList[gameManager.livesList.Count - 1].transform.SetParent(gameManager.livesParent.transform, false);
        }
    }

    public void LoseLive()
    {
        AddLives(-1);
        if (Lives < 0)
        {
            gameManager.GameOver();
        }
        else
        {
            Destroy(gameManager.livesList[gameManager.livesList.Count - 1]);
            gameManager.livesList.RemoveAt(gameManager.livesList.Count - 1);
            gameManager.player.Nuke();
            StartCoroutine(gameManager.FlashPlayer());
        }
    }

    public void AddBomb(int count)
    {
        BombCount += count;
        gameManager.bombText.text = BombCount + "";
    }

    public void NoBombs()
    {
        gameManager.bombText.color = Color.red;
    }
}
