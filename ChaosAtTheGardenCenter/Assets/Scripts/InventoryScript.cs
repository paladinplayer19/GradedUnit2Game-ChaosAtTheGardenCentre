using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject vegIcon;
    [SerializeField] private GameObject flowerIcon;
    [SerializeField] private GameObject defaultIcon;
    private PlayerController playerScript;
    private bool isHoldingItem;
   

    // Start is called before the first frame update
    void Start()
    {
        flowerIcon.SetActive(false);
        vegIcon.SetActive(false);
        playerScript = player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        isHoldingItem = playerScript.GetpickedUp();


        if (isHoldingItem == true)
        {
            if (playerScript.GetCurrentItem() == "Veg")
            {
               flowerIcon.SetActive(false);
               defaultIcon.SetActive(false);
               vegIcon.SetActive(true);
            }
            else if (playerScript.GetCurrentItem() == "Flower")
            {
               vegIcon.SetActive(false);
               defaultIcon.SetActive(false);
               flowerIcon.SetActive(true);

            }
            

        }
        else
        {
               flowerIcon.SetActive(false);
               vegIcon.SetActive(false);
               defaultIcon.SetActive(true);
        }

        

        
    }

   
}
