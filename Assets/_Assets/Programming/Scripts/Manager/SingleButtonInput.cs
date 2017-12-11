using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleButtonInput : MonoBehaviour {

    public float pressHoldThreshold = 0.3f;

    bool pressStart = false;
    float pressTime = 0;

    Player player;

    void Awake() {

        player = GetComponent<Player>();
    }

    public void CheckPlayerSingleInput(string inputKey) {


        if (pressStart) {
            pressTime += Time.deltaTime;
        } 

        // Case for button press - instant
        if(Input.GetButtonDown(inputKey))
        {
            player.SingleButtonPress();
        }

        // Case for button hold
        if(Input.GetButton(inputKey))
        {
            pressStart = true;

            //////
            /// - Only unique code here atm

            player.SingleButtonHold();

            ///
            //////

        }
        
        if (Input.GetButtonUp(inputKey)) {

            // Case for button press - counts if less than hold threshold
            if (pressTime <= pressHoldThreshold) {

                
            }
            pressStart = false;
            pressTime = 0;

            // Case for button release
            player.SingleButtonRelease();
        }
    }
}
