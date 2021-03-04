using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poses : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject opponent;

    PlayerMovement opponentMovement;

    //Flag which determines whether this is real player or CPU opponent
    public bool isCPU = false;

    //The amount of time it takes the AI to strike a pose
    public float AIPoseTime = 6f;

    int successfulInputCount = 0;
    public int successfulPoseCount = 0;

    public int keysPerPose = 3;

    AudioSource poseSound;

    // Start is called before the first frame update
    void Start()
    {
        //Find the opponents movement component
        opponentMovement = opponent.GetComponent<PlayerMovement>();

        poseSound = GetComponent<AudioSource>();

        if (isCPU)
        {
            //Start AI logic after start delay seconds
            Invoke("StartAIOpponent", gameManager.startDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.GAME)
        {
            if (!isCPU)
            {
                if (gameManager.allowInput)
                {
                    if (Input.GetButtonDown("PoseKey1"))
                    {
                        StrikeAPose();
                    }
                }
            }
        }
    }

    //Simply starts the AI striking a pose every certain amount of seconds
    void StartAIOpponent()
    {
        InvokeRepeating("StrikeAPose", AIPoseTime, AIPoseTime);
    }

    void StrikeAPose()
    {
        //If in the game
        if (gameManager.gameState == GameManager.GameState.GAME)
        {
            //Logs for testing at the moment
            if (isCPU)
            {
                Debug.Log("I am the AI opponent");
            }
            else
            {
                Debug.Log("I am the player");
                poseSound.Play();
            }
            successfulPoseCount += 1;
            Debug.Log("I have completed " + successfulPoseCount + " poses!");

            //Push opponent back on successful pose if not final pose
            if (successfulPoseCount <= 4)
            {
                opponentMovement.InitPushPlayer();
            }

            //Launch opponent if this player has won (reached 5 poses)
            if (successfulPoseCount >= 5)
            {
                opponentMovement.LaunchPlayer();
                gameManager.EndGame();
            }
        }
    }

    string SelectRandomPoseKey()
    {
        string poseKey = "";

        int randNum = Random.Range(1, 10);

        switch (randNum)
        {
            case 1:
                poseKey = "PoseKey1";
                break;
            case 2:
                poseKey = "PoseKey2";
                break;
            case 3:
                poseKey = "PoseKey3";
                break;
            case 4:
                poseKey = "PoseKey4";
                break;
            case 5:
                poseKey = "PoseKey5";
                break;
            case 6:
                poseKey = "PoseKey6";
                break;
            case 7:
                poseKey = "PoseKey7";
                break;
            case 8:
                poseKey = "PoseKey8";
                break;
            case 9:
                poseKey = "PoseKey9";
                break;
            case 10:
                poseKey = "PoseKey10";
                break;

        }

        Debug.Log(poseKey);

        return poseKey;
    }
}