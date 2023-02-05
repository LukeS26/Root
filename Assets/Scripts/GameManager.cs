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
    public GameObject instructionsScreen;
    public GameObject pauseMenu;
    public GameObject inGameUI;
    public GameObject beatLevelMenu;
    public GameObject loseLevelMenu;
    
    //InputManager Variables
    private InputManager inputManager;

    //Int Variables
    public int movesLeft;
    public int waterTiles = 1;

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
        if(movesLeft <= 0)
        {
            OpenLoseLevelMenu();
        }
        if(waterTiles <= 0)
        {
            OpenBeatLevelMenu();
        }
    }

    // Makes sure the player knows how many turns are left
    public void UpdateTurns()
    {
        // Makes sure that Start Menu doesn't try to access inaccessible object
        if(SceneManager.GetActiveScene().name != "Start Menu")
        {
            // Makes sure the UI always displays two digits for the remaining turns
            if(movesLeft < -9)
            {
                turnsText.text = "Turns Remaining:" + movesLeft;
            }
            else if(movesLeft < 0)
            {
                turnsText.text = "Turns Remaining: " + movesLeft;
            }
            else if(movesLeft < 10)
            {
                turnsText.text = "Turns Remaining: 0" + movesLeft;
            }
            else
            {
                turnsText.text = "Turns Remaining: " + movesLeft;
            }
        }
    }

    // Stop control of roots and switches to Lose Level Menu
    public void OpenLoseLevelMenu()
    {
        canMove = false;
        loseLevelMenu.SetActive(true);
        inGameUI.SetActive(false);
    }

    // Stop control of roots and switches to Lose Level Menu
    public void OpenBeatLevelMenu()
    {
        canMove = false;
        beatLevelMenu.SetActive(true);
        inGameUI.SetActive(false);
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
        // Reads the Movement input of the Plant as a vector 2, and stores them for use
        float x = inputManager.Plant.Movement.ReadValue<Vector2>().x;
        float y = inputManager.Plant.Movement.ReadValue<Vector2>().y;

        // Checks to see if there is no input, and if so allows a key to be pressed again
        if(Mathf.Abs(x) < 0.5f && Mathf.Abs(y) < 0.5f)
        {
            canMove = true;
        } 
        // Checks game isn't paused, key has been released, and there is movement that isn't in a diagonal direction, allow movement
        else if (!paused && canMove && Mathf.Abs(x - y) > 0.2f) 
        {
            canMove = false;
            Movement[] roots = FindObjectsOfType<Movement>(); // Finds all Movement scripts
            
            bool moved = false;

            // Checks that Y magnitude is the greater
            if(Mathf.Abs(y) > Mathf.Abs(x)) 
            {
                // Checks y is positive
                if(y > 0) 
                {   
                    // Loops over each movement script and moves roots up (that can)
                    foreach (Movement root in roots) 
                    {   
                        if(root.Move( new Vector2(0, 1) )) {
                            moved = true;
                        }
                    }
                }
                else 
                {
                    // Loops over each movement script and moves roots down (that can)
                    foreach (Movement root in roots) 
                    {
                        if(root.Move( new Vector2(0, -1) )) {
                            moved = true;
                        }
                    }
                }
            }
            else 
            {
                // Checks x is positive
                if(x > 0) 
                {
                    // Loops over each movement script and moves roots right (that can)
                    foreach (Movement root in roots) 
                    {
                        if( root.Move( new Vector2(1, 0) ) ) {
                            moved = true;
                        }
                    }
                } 
                else 
                {
                    // Loops over each movement script and moves roots left (that can)
                    foreach (Movement root in roots) 
                    {
                        if( root.Move( new Vector2(-1, 0) ) ) {
                            moved = true;
                        }
                    }
                }
            }

            if(moved) {
                movesLeft--;
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

    // Switches to Instructions Screen
    public void OpenInstructions()
    {
        warningScreen.SetActive(false);
        startScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        instructionsScreen.SetActive(true);
    }

    // Switches to Start Screen
    public void OpenStart()
    {
        warningScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        instructionsScreen.SetActive(false);
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

    // Switches to Level 3
    public void OpenLevel3()
    {
        SceneManager.LoadScene(3);
    }

    // Switches to Level 4
    public void OpenLevel4()
    {
        SceneManager.LoadScene(4);
    }

    // Switches to Level 5
    public void OpenLevel5()
    {
        SceneManager.LoadScene(5);
    }

    // Switches to Level 6
    public void OpenLevel6()
    {
        SceneManager.LoadScene(6);
    }

    // Switches to Level 7
    public void OpenLevel7()
    {
        SceneManager.LoadScene(7);
    }

    // Switches to Level 8
    public void OpenLevel8()
    {
        SceneManager.LoadScene(8);
    }

    // Switches to Level 9
    public void OpenLevel9()
    {
        SceneManager.LoadScene(9);
    }

    // Switches to Win Screen
    public void OpenWinScreen()
    {
        SceneManager.LoadScene(10);
    }
}
