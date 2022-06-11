using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{   
    // Declare private variables
    private string game =  "Game";
    private string welcome = "WelcomeScreen";
    private bool isGameOver;
    private PlayerController playerScript;
    private float timeSince;
    private InventoryScript inventoryScript;

    // Declare serialized private variables
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    
    void Start()
    {
        // Get components
        playerScript = player.GetComponent<PlayerController>();
        inventoryScript = canvas.GetComponent<InventoryScript>();
    }

  
    void Update()
    {
        // Gets current state of gameover
        isGameOver = playerScript.GetIsGameOver();

        // Checks if current scene is welcome
        if (SceneManager.GetActiveScene().name == welcome)
        {
            isGameOver = false;
            if (Input.GetKeyUp(KeyCode.K))
            {
                SceneManager.LoadScene(game);

            }
        }
        // Checks if current scene is game
        if (SceneManager.GetActiveScene().name == game)
        {
            
            if (isGameOver == true)
            {

                timeSince += 1f * Time.deltaTime;
                inventoryScript.DisplayFinalScore();

                // displays gameover screen for 5 seconds then loads welcome screen scene
                if (timeSince >= 5.0f)
                {
                    timeSince = 0;


                
                SceneManager.LoadScene(welcome);
                }

            }
        }
        
    }
}
