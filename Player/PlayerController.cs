using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Create variables
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public float bounceForce = 8f;

    private Vector3 moveDirection;

    public CharacterController charController;

    private Camera playerCam;

    public GameObject playerModel;
    public float rotationSpeed;

    public Animator anim;

    public bool isKnocking;
    public float knockbackLength = 0.5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;

    public bool stopMove;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking && !stopMove)
        {

            //Move player
            float yStore = moveDirection.y; //this variable will only work for this loop
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            //Make player jump when they press jump button
            if (charController.isGrounded)
            {
                moveDirection.y = -1f; //sets the gravity when player moves off platform the same as jumping

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }


            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            //transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed);

            charController.Move(moveDirection * Time.deltaTime);

            //Make character face correct direction of input
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, playerCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotaion = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                //playerModel.transform.rotation = newRotaion;

                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotaion, rotationSpeed * Time.deltaTime);
            }
        }

        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            //Move player
            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
               moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        //stop player movement when they reach the goal
        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection);
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z)); //Plays run animation when player moves
        anim.SetBool("Grounded", charController.isGrounded); //Plays jump animation if player is in air
    }

    //push player back when they get hurt
    public void knockback()
    {
        isKnocking = true;
        knockbackCounter = knockbackLength;
        Debug.Log("Knocked back");
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    //make player jump up when they jumpon crate or enemy
    public void bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }
}