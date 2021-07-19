using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthManager : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    public int deathSound;
    public GameObject deathEffect, droppedItem;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        currentHealth--;
        PlayerController.instance.bounce(); //Makes player jump a litle when they jump on enemy
        if (currentHealth <= 0)
        {
            audioManager.instance.playSFX(deathSound);
            Destroy(gameObject);
            PlayerController.instance.bounce(); //Makes player jump a litle when they jump on enemy
            Instantiate(deathEffect, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation); //plays effect when defeated
            Instantiate(droppedItem, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation); //drops item when defeated
        }
    }
}
