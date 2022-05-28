using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Animator anim;
    private string itemTag;
    private Vector3 dir;
    private bool canPick;
    private bool pickedUp;
    private RaycastHit hit;
    private bool isVeg;
    private bool isFlower;
    private bool isTable;
    private int vegCount;
    private int flowerCount;

    [SerializeField] private int spd = 10;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject[] flower = new GameObject[4];
    [SerializeField] private GameObject[] veg = new GameObject[4];
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

       if (Input.GetKeyDown(KeyCode.K))
       {
          
          PickUp(canPick);
            if (isTable == true)
            {
             PlaceItem(canPick);
            }
          
       }

    }

    public bool GetpickedUp()
    {
        return pickedUp;
    }

    public string GetItemTag()
    {
        return itemTag;
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
            canPick = false; /////////////////////////////////////// AFTER 1 DROPOFF ACTS LIKE NEXT PICKUP IS PLACING ON TABLE // FIX SO IT DOESNT STAY TABLE ACCESS
        }

        if (canPick == true)
        {
            
            if (hit.collider.tag.Equals("Veg"))
            {

                hit.collider.gameObject.SetActive(false);
                pickedUp = true;
                canPick = false;
                isVeg = true;
                isTable = false;
            }
            else
            {
                isVeg = false;
            }

            if (hit.collider.tag.Equals("Flower"))
            {

                hit.collider.gameObject.SetActive(false);
                pickedUp = true;
                canPick = false;
                isFlower = true;
                isTable = false;
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
                    veg[vegCount].SetActive(true);
                    pickedUp = false;
                    canPick = true;
                    vegCount++;
                    isTable = false;
                    Debug.Log("Dropped off");
                }

                if (isFlower == true)
                {
                    flower[flowerCount].SetActive(true);
                    pickedUp = false;
                    canPick = true;
                    flowerCount++;
                    isTable = false;
                    Debug.Log("Dropped off");
                }

                Debug.Log("hit table");

                
                
            }

            
        }
           
    }
}
