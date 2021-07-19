using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickup : MonoBehaviour
{
    public int value;
    public GameObject pickupEffect;

    public int soundToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //add coin to coin counter and remove object from the world when player touches it
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.AddCoins(value);
            Destroy(gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            audioManager.instance.playSFX(soundToPlay);
        }
    }
}
