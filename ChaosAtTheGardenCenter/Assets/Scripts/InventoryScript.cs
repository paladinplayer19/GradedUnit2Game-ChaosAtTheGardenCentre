using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject vegIcon;
    [SerializeField] private GameObject flowerIcon;
    [SerializeField] private GameObject defaultIcon;
    [SerializeField] private GameObject scoreTxt;
    [SerializeField] private GameObject scoreTxtBackground;
    [SerializeField] private GameObject finalScore;
    [SerializeField] private GameObject finalScoreBackground;

    private PlayerController playerScript;
    private bool isHoldingItem;
    
    private int score;
    

    private Text txt;
    private Text finalScoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        score = 600;
        txt = scoreTxt.GetComponent<Text>();
        finalScoreTxt = finalScore.GetComponent<Text>();
        txt.text = "Score: " + score;

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
       
        isHoldingItem = playerScript.GetPickedUp();


        if (playerScript.GetIsGameOver() == true && isHoldingItem)
        {
            flowerIcon.SetActive(false);
            vegIcon.SetActive(false);
        }
        else
        {

            if (isHoldingItem == true)
            {
                if (playerScript.GetCurrentItem() == "Veg")
                {
                    flowerIcon.SetActive(false);

                    vegIcon.SetActive(true);
                }
                else if (playerScript.GetCurrentItem() == "Flower")
                {
                    vegIcon.SetActive(false);

                    flowerIcon.SetActive(true);

                }


            }
            else
            {
                flowerIcon.SetActive(false);
                vegIcon.SetActive(false);

            }

        }
        
    }

    
    
    public void DisplayFinalScore()
    {
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

        if (scoreReduction == 100)
        {
            score -= 100;
            txt.text = "Score: " + score;

        }
        if (scoreReduction == 50)
        {
            score -= 50;
            txt.text = "Score: " + score;
        }
       

        

    }



}
