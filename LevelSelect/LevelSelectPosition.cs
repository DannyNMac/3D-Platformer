using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = Vector3.zero; //resets the player position
            PlayerController.instance.gameObject.SetActive(true);
        }
    }
}
