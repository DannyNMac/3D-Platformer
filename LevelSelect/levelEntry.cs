using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelEntry : MonoBehaviour
{
    public string lvlName;
    private bool loadLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && loadLevel)
        {
            SceneManager.LoadScene(lvlName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            loadLevel = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            loadLevel = false;
        }
    }
}
