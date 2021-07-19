using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    public bool isPressed, isOnOff;
    public Transform button, buttonDown;
    public Vector3 buttonUp;

    // Start is called before the first frame update
    void Start()
    {
        buttonUp = button.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Activate/deactivate when player presses button
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            if (isOnOff)
            {
                if (isPressed)
                {
                    button.position = buttonUp;
                    isPressed = false;
                }
                else
                {
                    button.position = buttonDown.position;
                    isPressed = true;
                }
            }
            else
            {
                button.position = buttonDown.position;
                isPressed = true;
            }
        }
    }
}
