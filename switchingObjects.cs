using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchingObjects : MonoBehaviour
{
    public GameObject theObject;
    public buttonController theButton;
    public bool revealWhenPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (theButton.isPressed)
        {
            theObject.SetActive(revealWhenPressed); //will be revealed when pressed (set to true)
        }
        else
        {
            theObject.SetActive(!revealWhenPressed); //won't be revealed (set to false)
        }
    }
}
