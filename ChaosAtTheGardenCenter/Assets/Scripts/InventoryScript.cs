using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController playerScript;
    private bool isHoldingItem;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        isHoldingItem = playerScript.GetpickedUp();

        
        if (isHoldingItem == true && playerScript.GetItemTag() == "Veg")
        {
            isHoldingItem = false;

           
        }
        else if (isHoldingItem == true && playerScript.GetItemTag() == "Flower")
        {
            isHoldingItem = false;
           
        }

    }

   
}
