using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AI_Controller : MonoBehaviour
{
    // Declare serialized private variables
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject caterpillar;
    [SerializeField] private GameObject snail;
    [SerializeField] private GameObject canvas;

    // Declare private variables 
    private PlayerController playerScript;
    private InventoryScript inventoryScript;
    private NavMeshAgent navMeshAgent_Snail;
    private NavMeshAgent navMeshAgent_Caterpillar;
    private LineRenderer snailPathLine;
    private LineRenderer caterpillarPathLine;
    private bool collidedVeg;
    private bool collidedFlower;
    private int collidedVegIndex;
    private int collidedFlowerIndex;
    
    private GameObject[] flowers = new GameObject[4];
    private GameObject[] vegs = new GameObject[4];

    private NavMeshPath path1;
    private NavMeshPath path2;
    // Start is called before the first frame update
    void Start()
    {
        // Gets components
        snailPathLine = snail.GetComponent<LineRenderer>();
        caterpillarPathLine = caterpillar.GetComponent<LineRenderer>();
        playerScript = player.GetComponent<PlayerController>();
        inventoryScript = canvas.GetComponent<InventoryScript>();

        path1 = new NavMeshPath();
        path2 = new NavMeshPath();
    }

    private void Awake()
    {
        // Get components
        navMeshAgent_Caterpillar = caterpillar.GetComponent<NavMeshAgent>();
        navMeshAgent_Snail = snail.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        // Gets current state of flowers and vegetables
        flowers = playerScript.GetFlowers();
        vegs = playerScript.GetVegs();

        // loops through vegetables
        for (int i = 0; i < vegs.Length; ++i)
        {
            // checks if current veg in loop is still active
           if (vegs[i].activeSelf == true)
           {
                // calculates a path between the current veg and the agent
                navMeshAgent_Snail.CalculatePath(vegs[i].transform.position, path1);
                
               
                // Rotates the model of the snail to where the the agent is looking so it looks realistic
               Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMeshAgent_Snail.steeringTarget);
                snail.transform.rotation = Quaternion.RotateTowards(snail.transform.rotation, toRotation, 10 * Time.deltaTime);

                // checks if snail collided with vegetable 
                if (collidedVeg == true)
                {
                    // deactivates veg eaten
                    vegs[collidedVegIndex].SetActive(false);

                    // checks to see if the next veg exists within the array
                    if (i+1 < vegs.Length)
                    {
                        // calculates a path between the veg after current veg and the agent
                        navMeshAgent_Snail.CalculatePath(vegs[i + 1].transform.position, path1);
                    
                    }
                    // resets state
                    collidedVeg = false;

                    // only deducted after collision if current scene is game
                    if (SceneManager.GetActiveScene().name == "Game")
                    {
                        inventoryScript.DecrementScore(100);
                    }
                }

           }

           // Sets the path the agent must follow
            navMeshAgent_Snail.SetPath(path1);

        }
        // loops through flowers
        for (int i = 0; i < flowers.Length; ++i)
        {
            // checks if current flower in loop is still active
            if (flowers[i].activeSelf == true)
            {
                // calculates a path between the current flower and the agent
                navMeshAgent_Caterpillar.CalculatePath(flowers[i].transform.position, path2);

                // Rotates the model of the caterpillar to where the the agent is looking so it looks realistic
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMeshAgent_Caterpillar.steeringTarget);
                caterpillar.transform.rotation = Quaternion.RotateTowards(caterpillar.transform.rotation, toRotation, 10 * Time.deltaTime);

                // checks if caterpillar collided with vegetable 
                if (collidedFlower == true)
                {
                    // deactivates flower eaten
                    flowers[collidedFlowerIndex].SetActive(false);

                    // checks to see if the next flower exists within the array
                    if (i + 1 < flowers.Length)
                    {
                        // calculates a path between the flower after current flower and the agent
                        navMeshAgent_Caterpillar.CalculatePath(flowers[i + 1].transform.position, path2);
                    }

                    // resets state
                    collidedFlower = false;

                    // only deducted after collision if current scene is game
                    if (SceneManager.GetActiveScene().name == "Game")
                    {
                        inventoryScript.DecrementScore(50);
                    }
                }

            }

            // Sets the path the agent must follow
            navMeshAgent_Caterpillar.SetPath(path2);
        }

       

        //DrawLine();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        // loops through all vegetables
        for (int i = 0; i < vegs.Length; ++i)
        {
            // checks if snail is the agent that collided
            if (this.name == snail.name)
            {
                if (collision.gameObject.name == vegs[i].name)
                {
                    collidedVeg = true;
                    collidedVegIndex = i;
                }
            }

        }
        // loops through all flowers
        for (int i = 0; i < flowers.Length; ++i)
        {
            // checks if caterpillar is the agent that collided
            if (this.name == caterpillar.name)
            {
                if (collision.gameObject.name == flowers[i].name)
                {
                collidedFlower = true;
                collidedFlowerIndex = i;
                }
            }
        }

        
    }

    private void DrawLine()
    {
        // Debug function to draw a line of the path the agents follow

        snailPathLine.positionCount = navMeshAgent_Snail.path.corners.Length;
        snailPathLine.SetPosition(0, snail.transform.position); 

        if (navMeshAgent_Snail.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 0; i < navMeshAgent_Snail.path.corners.Length; ++i)
        {
            Vector3 pointPosSnail = new Vector3(navMeshAgent_Snail.path.corners[i].x, navMeshAgent_Snail.path.corners[i].y, navMeshAgent_Snail.path.corners[i].z);

            snailPathLine.SetPosition(i, pointPosSnail);
        }

        caterpillarPathLine.positionCount = navMeshAgent_Caterpillar.path.corners.Length;
        caterpillarPathLine.SetPosition(0, caterpillar.transform.position); 

        if (navMeshAgent_Caterpillar.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 0; i < navMeshAgent_Caterpillar.path.corners.Length; ++i)
        {
            Vector3 pointPosCaterpillar = new Vector3(navMeshAgent_Caterpillar.path.corners[i].x, navMeshAgent_Caterpillar.path.corners[i].y, navMeshAgent_Caterpillar.path.corners[i].z);

            caterpillarPathLine.SetPosition(i, pointPosCaterpillar);
        }
    }
    
}
