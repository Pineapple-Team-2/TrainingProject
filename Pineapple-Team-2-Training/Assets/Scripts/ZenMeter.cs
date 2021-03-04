using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZenMeter : MonoBehaviour
{
    public GameObject player;

    Poses playerPoses;

    public Image zenMeter;

    public Sprite[] zenMeterTextures = new Sprite[6];

    //Start is called before the first frame update
    void Start()
    {
        playerPoses = player.GetComponent<Poses>();
    }

    //Update is called once per frame
    void Update()
    {
        //Set zen meter sprite based on player's successful pose count
        switch (playerPoses.successfulPoseCount)
        {
            case 0:
                zenMeter.sprite = zenMeterTextures[0];
                break;

            case 1:
                zenMeter.sprite = zenMeterTextures[1];
                break;

            case 2:
                zenMeter.sprite = zenMeterTextures[2];
                break;

            case 3:
                zenMeter.sprite = zenMeterTextures[3];
                break;

            case 4:
                zenMeter.sprite = zenMeterTextures[4];
                break;

            case 5:
                zenMeter.sprite = zenMeterTextures[5];
                break;

            default:
                zenMeter.sprite = zenMeterTextures[0];
                break;
        }
    }
}