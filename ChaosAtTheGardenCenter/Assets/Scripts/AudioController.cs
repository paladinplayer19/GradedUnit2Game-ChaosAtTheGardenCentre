using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Declare serialized private variables
    [SerializeField] private GameObject player;
    [SerializeField ]private AudioSource pickUpAudio;
    [SerializeField]private AudioSource dropOffAudio;

    // Declare private variables 
    private PlayerController playerScript;
    private RaycastHit hit;
    private bool isPickedUp;
    private bool canPlayDrop;
    private bool canPlayPick;

    // Start is called before the first frame update
    void Start()
    {
        // Gets component
        playerScript = player.GetComponent<PlayerController>();
        canPlayPick = true;
        canPlayDrop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Gets current state of hit and ispickedup variables
        hit = playerScript.GetHit();
        isPickedUp = playerScript.GetPickedUp();

        if (Input.GetKeyDown(KeyCode.K))
        {

            // doesnt let audio play if nothing was hit
            if (hit.collider != null)
            {
                // checks if an item was picked up
                if ((hit.collider.tag == "Veg" || hit.collider.tag == "Flower") && isPickedUp == true && canPlayPick == true)
                {
                    pickUpAudio.Play();
                    canPlayPick = false;
                    canPlayDrop = true;
                }
            }


            
            // doest let audio play if nothing was hit
            if (hit.collider != null)
            {
                // only allows if facing the table
              if (hit.collider.tag == "Drop")
              {
                    // checks if an item was picked up
                    if (hit.collider.tag == "Drop" && canPlayDrop == true)
                    {
                      dropOffAudio.Play();
                      canPlayDrop = false;
                      canPlayPick = true;
                    }
              }

            }
        }


    }
}
