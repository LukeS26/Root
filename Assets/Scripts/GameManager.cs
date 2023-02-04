using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameObject Variables
    public GameObject levelSelectScreen;
    public GameObject warningScreen;
    public GameObject startScreen;
    public GameObject infoScreen;
    public GameObject inGameUI;
    public GameObject eventHandler;
    public GameObject closeButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Hides Level Select Menu and Warns Player that This Level Hasn't Been Finished Yet
    public void WarnPlayer()
    {
        levelSelectScreen.SetActive(false);
        warningScreen.SetActive(true);
    }

    // Switches to Level Select Screen
    public void OpenLevelSelect()
    {
        warningScreen.SetActive(false);
        startScreen.SetActive(false);
        levelSelectScreen.SetActive(true);
    }

    // Switches to Start Screen
    public void OpenStart()
    {
        warningScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        startScreen.SetActive(true);
    }

    // Switches to Level 1
    public void OpenLevel1()
    {
        SceneManager.LoadScene(1);
    }
}
