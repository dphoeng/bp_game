using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameActive = false;
    public TextMeshProUGUI seedInput;
    public GameObject titleScreen;
    private string seed;

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
        gameActive = true;
        seed = seedInput.text == "" ? "default" : seedInput.text;
        Debug.Log(seedInput.text);
        titleScreen.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameActive = false;
        // TODO: game over screen
        //       destroy all objects on screen
    }
}
