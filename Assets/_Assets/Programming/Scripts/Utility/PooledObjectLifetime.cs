using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectLifetime : MonoBehaviour {

    public float lifetime = 0.0f;

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(DelayedReturn(lifetime));
	}
	
	IEnumerator DelayedReturn(float time) {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        yield return null;

        
    }
}
