using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMove : MonoBehaviour {

    public float despawnX = -16f;
    public bool moving = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if(moving && !SceneController.instance.GetSceneState(SceneController.SceneState.gameOver))
            transform.position = transform.position +  Vector3.left * Time.deltaTime * SceneController.instance.GetScrollSpeed();

        if (transform.position.x < despawnX ) {
            gameObject.SetActive(false);
        }
	}
}
