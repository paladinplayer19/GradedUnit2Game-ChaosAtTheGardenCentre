using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    [SerializeField ]private AudioSource pickUpAudio;
    [SerializeField]private AudioSource dropOffAudio;
    

    private PlayerController playerScript;
    private RaycastHit hit;
    private bool isPickedUp;
    private bool canPlayDrop;
    private bool canPlayPick;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
        canPlayPick = true;
        canPlayDrop = false;
    }

    // Update is called once per frame
    void Update()
    {
        hit = playerScript.GetHit();
        isPickedUp = playerScript.GetPickedUp();

        if (Input.GetKeyDown(KeyCode.K))
        {


            if (hit.collider != null)
            {
                if ((hit.collider.tag == "Veg" || hit.collider.tag == "Flower") && isPickedUp == true && canPlayPick == true)
                {
                    pickUpAudio.Play();
                    canPlayPick = false;
                    canPlayDrop = true;
                }
            }


            

            if (hit.collider != null)
            {
              if (hit.collider.tag == "Drop")
              {
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
