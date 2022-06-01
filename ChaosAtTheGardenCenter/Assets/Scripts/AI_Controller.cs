using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject caterpillar;
    [SerializeField] private GameObject snail;
    private PlayerController playerScript;
    private NavMeshAgent navMeshAgent_Snail;
    private NavMeshAgent navMeshAgent_Caterpillar;
    private bool collidedVeg;
    private bool collidedFlower;
    private int collidedVegIndex;
    private int collidedFlowerIndex;
    private GameObject[] flowers = new GameObject[4];
    private GameObject[] vegs = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
        
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
                navMeshAgent_Snail.destination = vegs[i].transform.position;
               

               Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMeshAgent_Snail.steeringTarget);
                snail.transform.rotation = Quaternion.RotateTowards(snail.transform.rotation, toRotation, 10 * Time.deltaTime);

                if (collidedVeg == true)
                {
                    vegs[collidedVegIndex].SetActive(false);
                    collidedVeg = false;
                    navMeshAgent_Snail.destination = vegs[i + 1].transform.position;
                }

           }
          


        }

        for (int i = 0; i < flowers.Length; ++i)
        {
            if (flowers[i].activeSelf == true)
            {
                navMeshAgent_Caterpillar.destination = flowers[i].transform.position;

                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMeshAgent_Caterpillar.steeringTarget);
                caterpillar.transform.rotation = Quaternion.RotateTowards(caterpillar.transform.rotation, toRotation, 10 * Time.deltaTime);

                if (collidedFlower == true)
                {
                    flowers[collidedFlowerIndex].SetActive(false);
                    collidedFlower = false;
                    navMeshAgent_Caterpillar.destination = flowers[i + 1].transform.position;
                }
            }
           
        }

        for (int i = 0; i < vegs.Length; ++i)
        {
            if (vegs[i].activeSelf == true)
            {
                return;
            }
            else
            {
                navMeshAgent_Snail.isStopped = true;
            }
        }

        for (int i = 0; i < flowers.Length; ++i)
        {
            if (flowers[i].activeSelf == true)
            {
                return;
            }
            else
            {
                navMeshAgent_Caterpillar.isStopped = true;
            }
        }

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
}
