using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private string game =  "Game";
    private string welcome = "WelcomeScreen";
    private bool isGameOver;
    private PlayerController playerScript;
    private float timeSince;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    private InventoryScript inventoryScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
        inventoryScript = canvas.GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = playerScript.GetIsGameOver();

        if (SceneManager.GetActiveScene().name == welcome)
        {
            isGameOver = false;
            if (Input.GetKeyUp(KeyCode.K))
            {
                SceneManager.LoadScene(game);

            }
        }
        if (SceneManager.GetActiveScene().name == game)
        {
            
            if (isGameOver == true)
            {
                timeSince += 1f * Time.deltaTime;
                inventoryScript.DisplayFinalScore();

                if (timeSince >= 5.0f)
                {
                    timeSince = 0;


                
                SceneManager.LoadScene(welcome);
                }

            }
        }
        
    }
}
