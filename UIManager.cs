using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;
    public float fadeSpeed = 1f;
    public bool fadeToBlack, fadeFromBlack;

    public Text healthText;
    public Image healthImage;

    public Text coinText;

    public GameObject pauseScreen, OptionScreen;

    public Slider musicVolSlider, sfxVolSlider;
    public string levelSelect, mainMenu;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { //when player dies spawn a black screen
        if(fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        
            if(blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
        //when player respawns remove the black screen
        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void resume()
    {
        GameManager.instance.pauseResume();
    }

    public void openOptions()
    {
        OptionScreen.SetActive(true);
    }

    public void closeOptions()
    {
        OptionScreen.SetActive(false);
    }
    
    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void setMusic()
    {
        audioManager.instance.setMusic();
    }

    public void setSFX()
    {
        audioManager.instance.setSFX();
    }
}
