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

    //InputManager Variables
    private InputManager inputManager;

    //Int Variables
    public int movesLeft;

    //Bool Variables
    private bool paused = false;
    private bool canMove = false;

    //TextMeshProUGUI Variables
    public TextMeshProUGUI turnsText;

    //Script Variables
    Movement movementScript;

    void Awake() 
    {
        inputManager = new InputManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateTurns();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTurns();
    }

    // Makes sure the player knows how many turns are left
    public void UpdateTurns()
    {
        // Makes sure that Start Menu doesn't try to access inaccessible object
        if(SceneManager.GetActiveScene().name != "Start Menu")
        {
            // Makes sure the UI always displays two digits for the remaining turns
            if(movesLeft < 10)
            {
                turnsText.text = "Turns Remaining: 0" + movesLeft;
            }
            else
            {
                turnsText.text = "Turns Remaining: " + movesLeft;
            }
        }
    }

    protected void OnEnable() 
    {
        inputManager.Plant.Enable();
    }

    protected void OnDisable() 
    {
        inputManager.Plant.Disable();
    }

    void FixedUpdate() 
    {   
        float x = inputManager.Plant.Movement.ReadValue<Vector2>().x;
        float y = inputManager.Plant.Movement.ReadValue<Vector2>().y;

        if(Mathf.Abs(x) < 0.5f && Mathf.Abs(y) < 0.5f)
        {
            canMove = true;
        } 
        else if (!paused && canMove && Mathf.Abs(x - y) > 0.2f) 
        {
            canMove = false;
            movesLeft--;

            Movement[] roots = FindObjectsOfType<Movement>();

            if(Mathf.Abs(y) > Mathf.Abs(x)) 
            {
                
                if(y > 0) 
                {
                    foreach (Movement root in roots) 
                    {
                        root.Move( new Vector2(0, 1) );
                    }
                }
                else 
                {
                    foreach (Movement root in roots) 
                    {
                        root.Move( new Vector2(0, -1) );
                    }
                }
            }
            else 
            {
                if(x > 0) 
                {
                    foreach (Movement root in roots) 
                    {
                        root.Move( new Vector2(1, 0) );
                    }
                } 
                else 
                {
                    foreach (Movement root in roots) 
                    {
                        root.Move( new Vector2(-1, 0) );
                    }
                }
            }
        }
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
        paused = true;
    }

    // Closes Pause Menu
    public void ClosePause()
    {
        pauseMenu.SetActive(false);
        inGameUI.SetActive(true);
        paused = false;
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
