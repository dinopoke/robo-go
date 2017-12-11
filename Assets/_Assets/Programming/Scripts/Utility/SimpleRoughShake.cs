using UnityEngine;
using System.Collections;

public class SimpleRoughShake : MonoBehaviour 
{

    Vector3 originalCameraPosition;

    float shake = 0;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public float shakeMagnitude;

    public Transform shakeCamera;

    void Awake() {

        originalCameraPosition = shakeCamera.localPosition;
    }

    void Update() {
  
        if (shake > 0) {
            shakeCamera.localPosition = (Vector3)(Random.insideUnitCircle * shakeAmount) + new Vector3(0,0,originalCameraPosition.z);
            shake -= Time.deltaTime * decreaseFactor;
        } 
        else {
            StopShaking();
            shake = 0f;
        }

    }

    public void StartShake() 
    {
        originalCameraPosition = shakeCamera.localPosition;
        shake = shakeMagnitude;
        

    }

    public void StartShake(float shakeAmount) 
    {
        originalCameraPosition = shakeCamera.localPosition;
        shake = shakeMagnitude;
        

    }


    void StopShaking()
    {
        shakeCamera.localPosition = originalCameraPosition;
    }

}