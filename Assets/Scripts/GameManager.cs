using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //GameObject Variables
    public GameObject levelSelectScreen;
    public GameObject warningScreen;
    public GameObject startScreen;
    public GameObject pauseMenu;
    public GameObject inGameUI;
    public GameObject root;

    //TextMeshProUGUI Variables
    public TextMeshProUGUI turnsText;

    //Script Variables
    Movement movementScript;
    
    // Start is called before the first frame update
    void Start()
    {
        movementScript = root.GetComponent<Movement>();
        turnsText.text = "Turns Remaining: " + "##";
    }

    // Update is called once per frame
    void Update()
    {
        turnsText.text = "Turns Remaining: " + "##"; // Makes sure the player knows how many turns are left
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

    // Opens Pause Menu
    public void OpenPause()
    {
        pauseMenu.SetActive(true);
        inGameUI.SetActive(false);
    }

    // Closes Pause Menu
    public void ClosePause()
    {
        pauseMenu.SetActive(false);
        inGameUI.SetActive(true);
    }

    // Switches to Start Screen
    public void LeaveLevel()
    {
        SceneManager.LoadScene(0);
    }

    // Switches to Level 1
    public void OpenLevel1()
    {
        SceneManager.LoadScene(1);
    }

    // Switches to Level 2
    public void OpenLevel2()
    {
        SceneManager.LoadScene(2);
    }
}
