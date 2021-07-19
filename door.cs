using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Transform theDoor, openRot;
    public float openSpeed;
    private Quaternion startRot;
    public buttonController theButton;

    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //actives when button is pressed
        if (theButton.isPressed)
        {
            theDoor.rotation = Quaternion.Slerp(theDoor.rotation, openRot.rotation, openSpeed * Time.deltaTime); //Opens door
        }
        else
        {
            theDoor.rotation = Quaternion.Slerp(theDoor.rotation, startRot, openSpeed * Time.deltaTime); //Closes door

        }
    }
}
