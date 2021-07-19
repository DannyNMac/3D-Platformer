using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platforms : MonoBehaviour
{
    public GameObject thePlatform;
    public GameObject thePlayer;

    //make player a child of the platform to move along with it
    void OnTriggerStay(Collider other)
    {
        thePlayer.transform.parent = thePlatform.transform;
    }

    //once off platform don't make it a child of platform
    void OnTriggerExit(Collider other)
    {
        thePlayer.transform.parent = null;
    }
}
