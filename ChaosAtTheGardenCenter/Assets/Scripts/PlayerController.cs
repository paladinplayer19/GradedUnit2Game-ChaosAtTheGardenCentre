using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    private Animator anim;
    private string itemTag;
    private Vector3 dir;
    private bool canPick;
    private bool pickedUp;
    private bool droppedOff;
    private RaycastHit hit;
    private bool isVeg;
    private bool isFlower;
    private bool isTable;
    private int vegCount;
    private int flowerCount;
    private string currentItem;
    private bool isGameOver;
    private int checkCountVeg;
    private int checkCountFlower;
   

    [SerializeField] private int spd = 10;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject[] droppedOffFlowers = new GameObject[4];
    [SerializeField] private GameObject[] droppedOffVeg = new GameObject[4];
    [SerializeField] private GameObject[] flowers = new GameObject[4];
    [SerializeField] private GameObject[] vegs = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pickedUp = false;
        canPick = true;

    }

    // Update is called once per frame
    void Update()
    {



       Move(playerModel);
       canPick = CheckItem();

        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (Input.GetKeyDown(KeyCode.K))
            {

                PickUp(canPick);
                if (isTable == true)
                {
                    PlaceItem(canPick);
                }

            }
        }
       

        CheckGameOver();

    }

    public bool GetPickedUp()
    {
        return pickedUp;
    }
    public bool GetDroppedOff()
    {
        return droppedOff;
    }

    public RaycastHit GetHit()
    {
        return hit;
    }
    public bool GetIsVeg()
    {
        return isVeg;
    }
    public bool GetIsFlower()
    {
        return isFlower;
    }
    public GameObject[] GetVegs()
    {
        return vegs;
    }

    public GameObject[] GetFlowers()
    {
        return flowers;
    }
    public string GetItemTag()
    {
        return itemTag;
    }
    public string GetCurrentItem()
    {
        return currentItem;
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }
    private void Move(GameObject playerModel)
    {
        

        if (rb.velocity == Vector3.zero)
        {
            anim.SetBool("isIdle", true);
        }

        
        
            if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                rb.transform.Translate(0.0f, 0.0f, spd * Time.deltaTime);
                playerModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                dir = Vector3.forward;
                anim.SetBool("isIdle", false);
                
            }
            



            if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {

                rb.transform.Translate(spd * Time.deltaTime, 0.0f, 0.0f);
                playerModel.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                dir = Vector3.right;
                anim.SetBool("isIdle", false);
                
            }
            



            if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                rb.transform.Translate(0.0f, 0.0f, -spd * Time.deltaTime);
                playerModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                dir = Vector3.back;
                anim.SetBool("isIdle", false);
                
            }
            


            if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                rb.transform.Translate(-spd * Time.deltaTime, 0.0f, 0.0f);
                playerModel.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                dir = Vector3.left;
                anim.SetBool("isIdle", false);
                
            }
  
    }

    private bool CheckItem()
    {
        Ray ray = new Ray(rb.transform.position, dir);

        
        if (Physics.Raycast(ray, out hit, 1))
        {
            if (hit.collider.tag.Equals("Veg"))
            {
                canPick = true;
                itemTag = "Veg";
            }
            else if (hit.collider.tag.Equals("Flower"))
            {
                canPick = true;
                itemTag = "Flower";
            }
            else if (hit.collider.tag.Equals("Drop"))
            {
                isTable = true;
            }
            
            }
        else
        {
            canPick = false;
        }
        

        Debug.DrawRay(ray.origin, ray.direction);

        return canPick;
    }
    private void PickUp(bool canPick)
    {
        if (pickedUp == true)
        {
            canPick = false;
        }

        if (canPick == true)
        {
            
            if (hit.collider.tag.Equals("Veg"))
            {   

                hit.collider.gameObject.SetActive(false);
                pickedUp = true;
                droppedOff = false;
                canPick = false;
                isVeg = true;
                isTable = false;
                currentItem = "Veg";
            }
            else
            {
                isVeg = false;
            }

            if (hit.collider.tag.Equals("Flower"))
            {

                hit.collider.gameObject.SetActive(false);
                pickedUp = true;
                droppedOff = false;
                canPick = false;
                isFlower = true;
                isTable = false;
                currentItem = "Flower";
            }
            else
            {
                isFlower = false;
            }



            Debug.Log("Picked Up");


        }
       
        
    }
    private void PlaceItem(bool canPick)
    {

        if (pickedUp == true)
        {
            
            if (isTable == true)
            {

                if (isVeg == true)
                {
                    droppedOffVeg[vegCount].SetActive(true);
                    pickedUp = false;
                    canPick = true;
                    vegCount++;
                    isTable = false;
                    Debug.Log("Dropped off");
                }

                if (isFlower == true)
                {
                    droppedOffFlowers[flowerCount].SetActive(true);
                    pickedUp = false;
                    canPick = true;
                    flowerCount++;
                    isTable = false;
                    Debug.Log("Dropped off");
                }

                currentItem = "Default";
                droppedOff = true;
                Debug.Log("hit table");

                
                
            }

            
        }
        

    }

    private void CheckGameOver()
    {



        for (int i = 0; i < vegs.Length; ++i)
        {
            if (vegs[i].activeSelf == true)
            {
                checkCountVeg = 0;
                return;
            }
            else
            {

                checkCountVeg = 1;
                
                
            }
        }

        for (int i = 0; i < flowers.Length; ++i)
        {
            if (flowers[i].activeSelf == true)
            {
                checkCountFlower = 0;
                return;
            }
            else
            {

                checkCountFlower = 1;
               
            }
        }

        

        if (checkCountFlower == 1 && checkCountVeg == 1)
        {
            Debug.Log("2");
            isGameOver = true;
        }
        else
        {
            
            isGameOver = false;
        }

        
    }
}
