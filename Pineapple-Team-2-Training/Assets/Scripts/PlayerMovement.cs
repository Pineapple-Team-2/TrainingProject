using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;

    //Flag which determines whether this is real player or CPU opponent
    public bool isCPU = false;

    Rigidbody playerRB;

    //Hold the players start and end position when getting pushed back
    Vector3 startPos, endPos;

    //Will change this once we have a better idea of the scene
    public float pushDistance = 3f;

    //Speed the player is pushed back at
    public float pushSpeed = 2f;

    //Flag to check if the player is moving
    private bool isMoving = false;

    //Timer to move along lerp
    private float lerpTimer = 0.0f;

    public Vector3 launchForce = new Vector3(-5f, 3f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        //Set the rigidbody
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Lerp if moving flag is true
        if (isMoving)
        {
            PushPlayer(pushDistance);
        }
    }

    //This should only happen once
    public void InitPushPlayer()
    {
        //Mark the players current position
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //If this is the real player
        if (!isCPU)
        {
            //End position is to the left of start position
            endPos = new Vector3(transform.position.x - pushDistance, transform.position.y, transform.position.z);
        }
        else
        {
            //End position is to the right of start position
            endPos = new Vector3(transform.position.x + pushDistance, transform.position.y, transform.position.z);
        }
   
        //Set moving flag to true and disable input
        isMoving = true;
        if (isCPU)
        {
            gameManager.allowInput = false;
        }
    }

    //This should update every frame whilst lerping
    void PushPlayer (float pushDistance)
    {
        //Pushes the player back along a smooth lerp at push speed
        if (lerpTimer < 1f)
        {
            //Increment lerp timer and move player along based on value
            lerpTimer += Time.deltaTime * pushSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, lerpTimer);
        }

        //Once player reaches end position
        if (transform.position == endPos)
        {
            //Reset moving flag and lerp timer, allow input again
            isMoving = false;
            lerpTimer = 0.0f;
            if (isCPU)
            {
                gameManager.allowInput = true;
            }
        }
    }

    //Launches the player off the beam, called on opponents final move
    public void LaunchPlayer ()
    {
        //Invert x direction if this is the CPU opponent
        if (!isCPU)
        {
            playerRB.AddForce(launchForce, ForceMode.Impulse);
        }
        else
        {
            playerRB.AddForce(launchForce.x * -1f, launchForce.y, launchForce.z, ForceMode.Impulse);
        }
    }
}