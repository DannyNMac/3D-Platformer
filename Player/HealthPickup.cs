﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHeal;
    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //heal player when they touch the heart item
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            if (isFullHeal)
            {
                HealthManager.instance.resetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(healAmount);
            }
        }
    }
}
