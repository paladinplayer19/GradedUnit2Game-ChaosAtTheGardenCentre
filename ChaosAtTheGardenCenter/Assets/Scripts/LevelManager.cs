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
    
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = playerScript.GetIsGameOver();

        if (SceneManager.GetActiveScene().name == welcome)
        {

            if (Input.GetKeyUp(KeyCode.K))
            {
                SceneManager.LoadScene(game);

            }
        }
        if (SceneManager.GetActiveScene().name == game)
        {
            
            if (isGameOver == true)
            {
                
                isGameOver = false;
                SceneManager.LoadScene(welcome);

            }
        }
        
    }
}
