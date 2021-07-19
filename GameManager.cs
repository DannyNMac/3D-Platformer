using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector3 respawnPosition;
    public GameObject deathEffect;
    public int currentCoins;
    public int levelEndMusic = 8;
    public string levelToLoad;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //Hides the cursor when game boots up
        Cursor.lockState = CursorLockMode.Locked;  //keeps the cursor in the centre of the screen

        //Set respawn position
        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0); //sets coin count to 0 on level start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseResume();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        HealthManager.instance.playerKilled();
    }

    //delay respawn
    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false); //Removes the player
        cameraController.instance.theCMBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f); //waits 2 seconds before respawning player

        HealthManager.instance.resetHealth();

        UIManager.instance.fadeFromBlack = true;

        PlayerController.instance.transform.position = respawnPosition; //moves the player to the respawn position
        cameraController.instance.theCMBrain.enabled = true;

        PlayerController.instance.gameObject.SetActive(true); //Adds the player back

        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeFromBlack = false;
    }

    //set the respawn point when player touches a checkpoint
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        //Debug.Log("Spawn set"); testing purpose
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
    }

    public void pauseResume()
    {
        if(UIManager.instance.pauseScreen.activeInHierarchy) //if it's active in the scene
        {
            UIManager.instance.pauseScreen.SetActive(false); //removes pause screen
            Time.timeScale = 1f; //resumes everything in scene
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true); //Displays pause menu
            UIManager.instance.closeOptions();
            Time.timeScale = 0f; //stops everything in the scene

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public IEnumerator LevelEndCo()
    {
        audioManager.instance.playMusic(levelEndMusic); //Plays victory music when player touches the goal
        PlayerController.instance.stopMove = true;
        yield return new WaitForSeconds(5f);
        //Debug.Log("Level Ended"); testing purpose
        SceneManager.LoadScene(levelToLoad);
    }
}
