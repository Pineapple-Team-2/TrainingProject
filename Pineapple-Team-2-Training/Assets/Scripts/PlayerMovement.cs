using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Hold the players start and end position when getting pushed back
    Vector3 startPos, endPos;

    //Will change this once we have a better idea of the scene
    public float pushDistance = 3f;

    //Speed the player is pushed back at
    public float pushSpeed = 2f;

    //Timer to move along lerp
    private float lerpTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mark push distance on key press (will change to do automatically after initial testing)
        if (Input.GetKeyDown("s"))
        {
            MarkPushDistance();
        }

        //Lerp object on key held (will change to do automatically using a bool flag after initial testing)
        if (Input.GetKey("s"))
        {
            pushPlayer(pushDistance);
        }

        //Reset lerp timer (will change to do automatically after initial testing)
        if (Input.GetKeyUp("s"))
        {
            lerpTimer = 0.0f;
        }
    }

    //This should only happen once
    void MarkPushDistance()
    {
        //Mark the players current position and the position they are to be pushed to
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos = new Vector3(transform.position.x - pushDistance, transform.position.y, transform.position.z);
    }

    //This should update every frame whilst lerping
    void pushPlayer (float pushDistance)
    {
        //Test output for the players start and end position
        Debug.Log("Player 1s start position is: " + startPos);
        Debug.Log("Player 1s end position is: " + endPos);

        //Pushes the player back along a smooth lerp at push speed
        if (lerpTimer < 1f)
        {
            lerpTimer += Time.deltaTime * pushSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, lerpTimer);
        }

        Debug.Log("Lerp Timer Value is: " + lerpTimer);
    }
}
