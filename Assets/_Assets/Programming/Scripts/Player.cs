using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SingleButtonInput))]
[RequireComponent(typeof(PlayerAudio))]


public class Player : MonoBehaviour {

    [Header("Variables")]
    public PlayerVariablesScriptableObject pVariables;

    Rigidbody2D rb;
    SingleButtonInput buttonInput;
    PlayerAudio playerAudio;

    SimpleRoughShake cam;

    ParticleSystem steam;

    XBoxController xboxController;

    string playerInputKey;

    bool camCooldown;


    // JUICE TIME BB

    void IdleJuice() {

    }
    void ButtonPressJuice() {
        xboxController.PressVibration();
        steam.Play();
    }

    void ButtonHoldJuice() {


    }

    void ButtonReleaseJuice() {
        xboxController.ReleaseVibration();

        steam.Stop();
    }
    void GoodPickupJuice() {

        xboxController.GoodPickupVibration();

        playerAudio.playPickUp();

    }

    void BadPickupJuice() {

        playerAudio.playHit();

        if (!camCooldown) {
            xboxController.BadPickupVibration();
            cam.StartShake();
            StartCoroutine(WaitShake());
        }
    }
    IEnumerator DieJuice() {

        cam.StartShake(0.5f);
        yield return xboxController.DeathVibration();



        // Not supposed to be here but oh well
        gameObject.SetActive(false);
    }
    

    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<SimpleRoughShake>();
        playerAudio = GetComponent<PlayerAudio>();
        buttonInput = GetComponent<SingleButtonInput>();

        xboxController = GetComponent<XBoxController>();

        steam = transform.Find("Steam").GetComponent<ParticleSystem>();
        steam.Stop();


        playerInputKey = name + "Move";

        rb.gravityScale = pVariables.gravity;

    }

    void FixedUpdate()
    {
        if(SceneController.instance.GetSceneState(SceneController.SceneState.gameRunning)) {

            // Check Player Input
            buttonInput.CheckPlayerSingleInput(playerInputKey);

        }
        else if(SceneController.instance.GetSceneState(SceneController.SceneState.startPhase)) {

            // Start up phase - TODO

            if (Input.GetButtonDown(playerInputKey)) {
                xboxController.PressVibration();
                steam.Play();

            }

            if(Input.GetButton(playerInputKey))
            {

                


                JetpackBoost();
                rb.AddForce((Vector2.right) * 100f);

                // Velocity cap stuff here
                if (rb.velocity.x > Vector2.right.x ) {
                    rb.velocity = new Vector2(1, rb.velocity.y);
                }
            }

            if(Input.GetButtonUp(playerInputKey)) {
                xboxController.ReleaseVibration();

                steam.Stop();
            }

        }

        CheckPlayerLocation();
       
    }
    
    public void SingleButtonPress() {

        ButtonPressJuice();

    }

    public void SingleButtonHold() {

        ButtonHoldJuice();

        if (rb.velocity.y < pVariables.jetpackSpeedCap) {

            JetpackBoost();
            FuelDecrease();
        }

    }

    public void SingleButtonRelease() {


        ButtonReleaseJuice();


    }



    void JetpackBoost() {

        rb.AddForce((Vector2.up) *  pVariables.jetpackForce);
    }

    void OnTriggerEnter2D(Collider2D col) {

        CheckPickup(col);


    }

    void CheckPickup(Collider2D col) {

        // Needs better identifers than strings here

        if (col.transform.tag.Equals("Red Pickup") && name.Equals("Player2")) {

            GoodPickup(col);
        }
        else if (col.transform.tag.Equals("Blue Pickup") && name.Equals("Player1")) {

            GoodPickup(col);
        }
        else{
            BadPickup(col);
        }

    }

    void GoodPickup(Collider2D col) {

        GoodPickupJuice();

        StartCoroutine(MovePlayer(pVariables.goodPickupIncrease));

        col.gameObject.SetActive(false);
    }

    void BadPickup(Collider2D col) {

        BadPickupJuice();

        StartCoroutine(MovePlayer(pVariables.badPickupDecrease));

        col.gameObject.SetActive(false);

    }

    IEnumerator MovePlayer(float speed, float time = 0.5f) {

        rb.velocity += new Vector2(speed, 0);

        float t = 0;

        // Gotta fix this - needs to ease into a stop
        while (t < time + Time.deltaTime) {
            t += Time.deltaTime;

            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
            yield return null;
        }

        StallPlayer();
        yield return null;
    }

    IEnumerator WaitShake() {
        // Cooldown for screen shake

        camCooldown = true;
        yield return new WaitForSeconds(pVariables.shakeCooldown);
        camCooldown = false;
        yield return null;
    }

    void FuelDecrease() {

        if (rb.velocity.x > pVariables.backwardSpeedCap) {
            rb.velocity += new Vector2(pVariables.fuelDepletionSpeed * Time.deltaTime, 0);
        }

    }

    void CheckPlayerLocation() {


        // Cap player at max x position
        if (rb.position.x > pVariables.maxX) {
            StallPlayer();
        }

        // Check if player is out of bounds
        if (rb.position.x < pVariables.deathX) {
            PlayerDeath();
        } 
        else if (rb.position.y < pVariables.deathY) {
            PlayerDeath();
        }

        // Start game when player has passed startX 
        if(rb.position.x > pVariables.startX && SceneController.instance.GetSceneState(SceneController.SceneState.startPhase)) {

            // Stop velocity
            StallPlayer();
            SceneController.instance.StartGame();

        }
    }

    void StallPlayer() {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void PlayerDeath() {

        SceneController.instance.PlayerDied(name);

        StartCoroutine(DieJuice()); 


    }

    void OnDisable() {

        xboxController.StopVibration();

    }

}
