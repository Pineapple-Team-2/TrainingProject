using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        PREGAME,
        GAME,
        WINORLOSE
    }

    public GameState gameState;

    public float startDelay = 3.0f;

    public bool allowInput = false;

    public GameObject backgroundPlane, player1, player2, zenPanel, winLosePanel, winText, loseText;

    public Material[] backgroundMaterials = new Material[3];

    AudioSource startEndGong;

    //Start is called before the first frame update
    void Start()
    {
        //Set initial game state
        gameState = GameState.PREGAME;

        //Randomly set the background
        int randNum = Random.Range(0, 2);
        backgroundPlane.GetComponent<MeshRenderer>().material = backgroundMaterials[randNum];

        //Find audio clip
        startEndGong = GetComponent<AudioSource>();

        //Start game after 3 secs
        Invoke("StartGame", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If time to put a menu in, this will be run on pressing play
    //If not, just have it run after a small delay in start
    void StartGame()
    {
        gameState = GameState.GAME;
        startEndGong.Play();
        zenPanel.SetActive(true);
        allowInput = true;
    }

    //This will be run if either player falls off of the beam at any time
    //Switches game state to endgame, plays win sound and shows win or lose UI
    public void EndGame()
    {
        gameState = GameState.WINORLOSE;
        startEndGong.Play();

        Invoke("WinCheck", 2f);
        Invoke("RestartGame", 5f);
    }

    void WinCheck()
    {
        //Setup win lose UI based on who won
        if (player1.transform.position.x < -7.5f)
        {
            winText.SetActive(false);
            loseText.SetActive(true);
        }
        else
        {
            winText.SetActive(true);
            loseText.SetActive(false);
        }
        winLosePanel.SetActive(true);
    }

    //Restart the game by opening current level again
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}