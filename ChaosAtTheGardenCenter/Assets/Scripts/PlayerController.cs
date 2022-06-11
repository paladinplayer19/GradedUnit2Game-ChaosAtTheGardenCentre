using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Declare private variables
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
 

    // Declare serialized private variables
    [SerializeField] private int spd = 10;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject playerModel;
    
    [SerializeField] private GameObject[] droppedOffFlowers = new GameObject[4];
    [SerializeField] private GameObject[] droppedOffVeg = new GameObject[4];
    [SerializeField] private GameObject[] flowers = new GameObject[4];
    [SerializeField] private GameObject[] vegs = new GameObject[4];
    [SerializeField] private GameObject caterpillar;
    [SerializeField] private GameObject snail;


   
    void Start()
    {
        //Get components
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // Set action states
        pickedUp = false;
        canPick = true;

    }

  
    void Update()
    {

        

       
       canPick = CheckItem();

        // make sure current scene is game
        if (SceneManager.GetActiveScene().name == "Game")
        {
            
            if (Input.GetKeyDown(KeyCode.K))
            {

                PickUp(canPick);

                // checks if player is facing the table
                if (isTable == true)
                {
                    PlaceItem(canPick);
                }

            }

         CheckGameOver();

        }

        Move(playerModel);
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
        // Sets the velocity to zero everyframe
        rb.velocity = Vector3.zero;

        // Check to see if the player is idle then sets idle to true
        if (rb.velocity == Vector3.zero)
        {
            anim.SetBool("isIdle", true);
        }

        
        
            // moves player up if either key is pressed
            if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                rb.transform.Translate(0.0f, 0.0f, spd * Time.deltaTime);
                playerModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                dir = Vector3.forward;
                anim.SetBool("isIdle", false);
                
            }
            


            // moves player right if either key is pressed
            if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {

                rb.transform.Translate(spd * Time.deltaTime, 0.0f, 0.0f);
                playerModel.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                dir = Vector3.right;
                anim.SetBool("isIdle", false);
                
            }
            


            // moves player down if either key is pressed
            if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                rb.transform.Translate(0.0f, 0.0f, -spd * Time.deltaTime);
                playerModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                dir = Vector3.back;
                anim.SetBool("isIdle", false);
                
            }
            

            // moves player left if either key is pressed
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
        // Creates ray
        Ray ray = new Ray(rb.transform.position, dir);

        // casts a ray to see whats in front
        if (Physics.Raycast(ray, out hit, 1))
        {
            // checks to see if raycast hit is a veg
            if (hit.collider.tag.Equals("Veg"))
            {
                canPick = true;
                itemTag = "Veg";
            }
            // checks to see if raycast hit is a flower
            else if (hit.collider.tag.Equals("Flower"))
            {
                canPick = true;
                itemTag = "Flower";
            }
            // checks to see if raycast hit is the table
            else if (hit.collider.tag.Equals("Drop"))
            {
                isTable = true;
            }

        }
        else
        {
            canPick = false;
        }
        

       // Debug.DrawRay(ray.origin, ray.direction);

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
            // if object from raycast is veg then deactivate it
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
            // if object from raycast is flower then deactivate it
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



           // Debug.Log("Picked Up");


        }
       
        
    }
    private void PlaceItem(bool canPick)
    {
        // checks if player is holding an item
        if (pickedUp == true)
        {
            // checks if player is looking at the table 
            if (isTable == true)
            {
                // checks if held item is veg
                if (isVeg == true)
                {
                    droppedOffVeg[vegCount].SetActive(true);
                    pickedUp = false;
                    canPick = true;
                    vegCount++;
                    isTable = false;
                    //Debug.Log("Dropped off");
                }

                // checks if held item is flower
                if (isFlower == true)
                {

                    droppedOffFlowers[flowerCount].SetActive(true);
                    pickedUp = false;
                    canPick = true;
                    flowerCount++;
                    isTable = false;
                   // Debug.Log("Dropped off");
                }

                currentItem = "Default";
                droppedOff = true;
               // Debug.Log("hit table");

                
                
            }

            
        }
        

    }

    private void CheckGameOver()
    {


        // Loops through all vegs and checks if any veg are still existing
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
        // Loops through all flowers and checks if any flower are still existing
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

        
        // Checks to see if all plants are deactivated and decided if its gameover or not
        if (checkCountFlower == 1 && checkCountVeg == 1)
        {
            
            isGameOver = true;
        }
        else
        {
            
            isGameOver = false;
        }

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Makes sure there is not problems with collision
        if (collision.gameObject.name == snail.name || collision.gameObject.name == caterpillar.name)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
        }
        if (collision.collider.tag.Equals("Drop"))
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
        }
        if (collision.collider.tag.Equals("Obstacle"))
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
        }
    }
}
