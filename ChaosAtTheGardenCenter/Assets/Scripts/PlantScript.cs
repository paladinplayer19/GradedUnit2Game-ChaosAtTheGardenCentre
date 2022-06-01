using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController playerScript;
    private GameObject[] flowers = new GameObject[4];
    private GameObject[] vegs = new GameObject[4];


    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        flowers = playerScript.GetFlowers();
        vegs = playerScript.GetVegs();
    }

    private void DecrementScore()
    {
        // veg == 100 deducted
        // flower == 50 deducted
    }
    
}
