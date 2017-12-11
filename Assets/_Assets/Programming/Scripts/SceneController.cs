using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneController : MonoBehaviour {

    public static SceneController instance;
    public enum SceneState {gameOver, startPhase, gameRunning, paused}

    SceneState currentSceneState;

    [Header("Variables")]
    public GameVariablesScriptableObject gVariables;


    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    string p1InputKey;
    string p2InputKey;
    
    
    // Kinda tacky    
    bool p1dead;
    bool p2dead;

    float scrollSpeed;

    float gameTimer = 0;

    [Header("Animations")]
    public Animation sunAnim;
    public Animation sceneLightAnim;
    public Animation titleAnim;

    [Header("Managers")]
    public GameObject gameManager;
    public GameObject audioManager;

    public SceneFader sceneFader;



	// Use this for initialization
	void Awake () {

        instance = this;

        currentSceneState = SceneState.paused;


        CheckForManagers();

        StartCoroutine(SetUpGame());
        GetPlayers(); 

        // Legacy animations setup
        sceneLightAnim.clip.legacy = true;
        titleAnim.clip.legacy = true;

	}



    void CheckForManagers() {
        if (GameManager.instance == null) {
            Instantiate(gameManager);
        }
        if (AudioManager.instance == null) {
            Instantiate(audioManager);
        }
    }

    void GetPlayers() {

        // Too stringy

        //player1 = GameObject.Find("Player1");
        //player2 = GameObject.Find("Player2");


        p1InputKey = player1.name + "Move";
        p2InputKey = player2.name + "Move";		
    }
	
	// Update is called once per frame
	void Update () {

        // Check Game State
        
        if (currentSceneState == SceneState.startPhase) {

        }

        else if (currentSceneState == SceneState.gameRunning){
            gameTimer += Time.deltaTime;
        }

        else if (currentSceneState == SceneState.gameOver) {

            if (Input.GetButton(p1InputKey) || Input.GetButton(p2InputKey)) {
                StartCoroutine(ReloadScene());
            }
        }
        else if(currentSceneState == SceneState.paused) {

        }		
	}

     IEnumerator SetUpGame() {

        AudioManager.instance.PlayTrack(1);

        yield return sceneFader.FadeIn();

        currentSceneState = SceneState.startPhase;

        p1dead = false;
        p2dead = false;

        gameTimer = 0;

        yield return null;

    }

    public void StartGame() {

        if (currentSceneState == SceneState.startPhase) {

            scrollSpeed = gVariables.scrollSpeed;

            SpawnManager.instance.Begin();

            StartGameAnimations();
            
            currentSceneState = SceneState.gameRunning;

        }


    }

    void StartGameAnimations() {
        sunAnim.enabled = true;
        sceneLightAnim.enabled = true;

        sunAnim.Play();
        sceneLightAnim.Play();


        titleAnim.Play("Title_Disappear");
    } 

    void PauseGameAnimations() {
        sunAnim.enabled = false;
        sceneLightAnim.enabled = false;
    } 

    public void PlayerDied(string name) {

        // Too stringy

        if (name == player1.name) {
            instance.p1dead = true;
        }
        else if (name == player2.name) {
            instance.p2dead = true;
        }

        if (instance.p1dead && instance.p2dead) {
            SetGameOver();
        }
    }


     void SetGameOver() {


        SpawnManager.instance.End();
        AudioManager.instance.FadeOutTrack(1, 1f);

        currentSceneState = SceneState.gameOver;    
        PauseGameAnimations();
    }


    IEnumerator ReloadScene() {


        yield return StartCoroutine(sceneFader.FadeOut());

        ObjectPooler.instance.DeactivatePool();
        PatternPooler.instance.DeactivatePool();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null;
    }

    public SceneState GetSceneState() {

        return currentSceneState;
    }

    public bool GetSceneState(SceneState state) {

        if(currentSceneState == state) {
            return true;
        }
        else {
            return false;
        }

    }

    public float GetScrollSpeed() {
        return scrollSpeed;
    }

    public float GetGameTime() {
        return gameTimer;
    }

    public string GetFurthestPlayer() {
        // This is really messy

        if(!p1dead && !p2dead) {
            if(player1.transform.position.x > player2.transform.position.x) {
                return player1.name;
            }
            else {
                return player2.name;
            }
        }
        else {
            return null;
        }
    }    

}
