using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public string firstSceneName = "Game";

    // Use this for initialization
    void Awake () {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            DestroyObject(gameObject);
        }

        if(SceneManager.GetActiveScene().name == "Init") {
            SceneManager.LoadSceneAsync(firstSceneName);
        }
          
	}

	// Update is called once per frame
	 void Update () {

	}
}
