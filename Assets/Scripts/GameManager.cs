using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameActive = false;
    public GameObject seedInput;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        int seed;

        gameActive = true;

        if (int.TryParse(seedInput.GetComponent<TMP_InputField>().text, out seed))
		{
            Random.InitState(seed);
        } else
		{
            Random.InitState(0);
        }
        
        titleScreen.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameActive = false;
        // TODO: game over screen
        //       destroy all objects on screen
    }
}
