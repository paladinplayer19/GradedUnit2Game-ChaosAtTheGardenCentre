using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject caterpillar;
    [SerializeField] private GameObject snail;
    [SerializeField] private GameObject canvas;
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
        snailPathLine = snail.GetComponent<LineRenderer>();
        caterpillarPathLine = caterpillar.GetComponent<LineRenderer>();

        path1 = new NavMeshPath();
        path2 = new NavMeshPath();
        playerScript = player.GetComponent<PlayerController>();
        inventoryScript = canvas.GetComponent<InventoryScript>();
    }

    private void Awake()
    {
        navMeshAgent_Caterpillar = caterpillar.GetComponent<NavMeshAgent>();
        navMeshAgent_Snail = snail.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        flowers = playerScript.GetFlowers();
        vegs = playerScript.GetVegs();

        for (int i = 0; i < vegs.Length; ++i)
        {
           if (vegs[i].activeSelf == true)
           {
                navMeshAgent_Snail.CalculatePath(vegs[i].transform.position, path1);
                
               

               Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMeshAgent_Snail.steeringTarget);
                snail.transform.rotation = Quaternion.RotateTowards(snail.transform.rotation, toRotation, 10 * Time.deltaTime);

                if (collidedVeg == true)
                {
                    vegs[collidedVegIndex].SetActive(false);


                    if (i+1 < vegs.Length)
                    {

                    navMeshAgent_Snail.CalculatePath(vegs[i + 1].transform.position, path1);//check if next path is within veg.length cause i +1 is bad juju
                    
                    }
                    collidedVeg = false;

                    if (SceneManager.GetActiveScene().name == "Game")
                    {
                        inventoryScript.DecrementScore(100);
                    }
                }

           }


            navMeshAgent_Snail.SetPath(path1);

        }

        for (int i = 0; i < flowers.Length; ++i)
        {
            if (flowers[i].activeSelf == true)
            {
                navMeshAgent_Caterpillar.CalculatePath(flowers[i].transform.position, path2);
                

                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMeshAgent_Caterpillar.steeringTarget);
                caterpillar.transform.rotation = Quaternion.RotateTowards(caterpillar.transform.rotation, toRotation, 10 * Time.deltaTime);

                if (collidedFlower == true)
                {
                    flowers[collidedFlowerIndex].SetActive(false);

                    if (i + 1 < flowers.Length)
                    {
                        navMeshAgent_Caterpillar.CalculatePath(flowers[i + 1].transform.position, path2);
                    }


                    collidedFlower = false;

                    if (SceneManager.GetActiveScene().name == "Game")
                    {
                        inventoryScript.DecrementScore(50);
                    }
                }

            }

            navMeshAgent_Caterpillar.SetPath(path2);
        }

       

        //DrawLine();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        for (int i = 0; i < vegs.Length; ++i)
        {
            if (this.name == snail.name)
            {
                if (collision.gameObject.name == vegs[i].name)
                {
                    collidedVeg = true;
                    collidedVegIndex = i;
                }
            }

        }
        for (int i = 0; i < flowers.Length; ++i)
        {
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
        snailPathLine.positionCount = navMeshAgent_Snail.path.corners.Length;
        snailPathLine.SetPosition(0, snail.transform.position); /// might need target pos

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
        caterpillarPathLine.SetPosition(0, caterpillar.transform.position); /// might need target pos

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
