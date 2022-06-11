using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{   
    // Declare serialized private variables
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject vegIcon;
    [SerializeField] private GameObject flowerIcon;
    [SerializeField] private GameObject defaultIcon;
    [SerializeField] private GameObject scoreTxt;
    [SerializeField] private GameObject scoreTxtBackground;
    [SerializeField] private GameObject finalScore;
    [SerializeField] private GameObject finalScoreBackground;

    // Declare private variables
    private PlayerController playerScript;
    private bool isHoldingItem;
    private int score;
    private Text txt;
    private Text finalScoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        // Gets score components
        score = 600;
        txt = scoreTxt.GetComponent<Text>();
        finalScoreTxt = finalScore.GetComponent<Text>();
        txt.text = "Score: " + score;


        // Sets up UI
        defaultIcon.SetActive(true);
        scoreTxtBackground.SetActive(true);
        scoreTxt.SetActive(true);
        flowerIcon.SetActive(false);
        vegIcon.SetActive(false);
        finalScore.SetActive(false);
        finalScoreBackground.SetActive(false);
        playerScript = player.GetComponent<PlayerController>();
        


    }

    // Update is called once per frame
    void Update()
    {
       // Gets current state of isholdingitem
        isHoldingItem = playerScript.GetPickedUp();


        // checks if game is over and is still holding item, then deactivates UI icons
        if (playerScript.GetIsGameOver() == true && isHoldingItem)
        {
            // 
            flowerIcon.SetActive(false);
            vegIcon.SetActive(false);
        }
        else
        {
            // checks if player is holding item or not
            if (isHoldingItem == true)
            {
                // checks if player is holding a veg or not, then changes UI
                if (playerScript.GetCurrentItem() == "Veg")
                {
                    flowerIcon.SetActive(false);

                    vegIcon.SetActive(true);
                }
                // checks if player is holding a flower or not, then changes UI
                else if (playerScript.GetCurrentItem() == "Flower")
                {
                    vegIcon.SetActive(false);

                    flowerIcon.SetActive(true);

                }


            }
            else
            {
                // makes UI blank if not holding item
                flowerIcon.SetActive(false);
                vegIcon.SetActive(false);

            }

        }
        
    }

    
    
    public void DisplayFinalScore()
    {
        // Deactivates game UI and activates gameover screen UI
        flowerIcon.SetActive(false);
        vegIcon.SetActive(false);
        scoreTxtBackground.SetActive(false);
        scoreTxt.SetActive(false);
        defaultIcon.SetActive(false);
        finalScore.SetActive(true);
        finalScoreBackground.SetActive(true);
        finalScoreTxt.text = "Score: " + score;
        
    }
    public void DecrementScore(int scoreReduction)
    {
        // Reduces score by 100
        if (scoreReduction == 100)
        {
            score -= 100;
            txt.text = "Score: " + score;

        }
        // Reduces score by 50
        if (scoreReduction == 50)
        {
            score -= 50;
            txt.text = "Score: " + score;
        }
       

        

    }



}
