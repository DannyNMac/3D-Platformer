using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelExit : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //do animation and go to next level when player touches the goal
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetTrigger("touch"); 
            StartCoroutine(GameManager.instance.LevelEndCo());
        }
    }
}
