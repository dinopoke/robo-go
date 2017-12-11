using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPattern : MonoBehaviour {

    public bool moving = true;

    public bool dynamicallyGenerate = false;

	// Use this for initialization
	void Awake () {

        if (dynamicallyGenerate){
            ClearPattern();
            GeneratePattern();
        }

	}
	
    public virtual void PatternSetup(GameObject pickUp) {
        foreach (Transform child in transform) {

            foreach (GameObject trash in child) {
                Destroy(trash);
            }

            GameObject obj = Instantiate(pickUp, child.position, child.rotation);
            obj.transform.SetParent(child);

            // Stripping code here for now - will change later
            obj.GetComponent<PickupMove>().enabled = false;

        }
    }

    public virtual void GeneratePattern() {


    }

    void ClearPattern() {

        foreach (Transform child in transform) {
            Destroy(child);
        }
    }


	// Update is called once per frame
	void Update () {	
        if(!SceneController.instance.GetSceneState(SceneController.SceneState.gameOver)) {
            if(moving) {
                transform.position = transform.position +  Vector3.left * Time.deltaTime * SceneController.instance.GetScrollSpeed();
            }
            PatternMovement();
        }
	}

    public virtual void PatternMovement() {


    }
}
