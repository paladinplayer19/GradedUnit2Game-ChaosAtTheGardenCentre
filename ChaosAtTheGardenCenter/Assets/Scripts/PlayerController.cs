using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 playerInput;
    private Vector3 movement;
    private Animator anim;
    [SerializeField] private int spd = 10;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject playerModel;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Move(playerModel);

    }

    private void Move(GameObject playerModel)
    {

        if (rb.velocity == Vector3.zero)
        {
            anim.SetBool("isIdle",true);
        }
        if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
        {
            rb.transform.Translate(0.0f, 0.0f, spd * Time.deltaTime);
            playerModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            anim.SetBool("isIdle", false);
        }
     


        if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {

            rb.transform.Translate(spd * Time.deltaTime, 0.0f, 0.0f);
            playerModel.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            anim.SetBool("isIdle", false);
        }
     


        if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
        {
            rb.transform.Translate(0.0f, 0.0f, -spd * Time.deltaTime);
            playerModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            anim.SetBool("isIdle", false);
        }
        

        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            rb.transform.Translate(-spd * Time.deltaTime, 0.0f, 0.0f);
            playerModel.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
            anim.SetBool("isIdle", false);
        }
        
    }

}
