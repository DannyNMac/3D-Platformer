﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currentHealth, maxHealth;

    public float invincibleLength = 2f;
    private float invinceCounter;

    public Sprite[] healthBarImages;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        resetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(invinceCounter > 0)
        {
            invinceCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if(Mathf.Floor(invinceCounter * 5) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);

                }

                if(invinceCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);

                }
            }        
        }
    }

    public void Hurt()
    {
        if (invinceCounter <= 0)
        {
            currentHealth--; //Takes 1 hp away from current health

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.knockback();
                invinceCounter = invincibleLength;

                for(int i =0; i < PlayerController.instance.playerPieces.Length; i++)
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }
            }
            UpdateUI();
        }
    }

    public void resetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateUI();
    }

    //Display health
    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();

        switch(currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;

            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;

            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;

            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;

            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }

    public void playerKilled()
    {
        currentHealth = 0;
        UpdateUI();
    }
}
