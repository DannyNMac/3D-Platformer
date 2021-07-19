using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public string firstLevel, levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel); //Loads the first level when the player selects the start button
    }

    public void continueGame()
    {
        SceneManager.LoadScene(levelSelect); //loads the level select scene
    }

    public void QuitGame()
    {
        Application.Quit(); //Will close the game when the final build is done
    }
}
