using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    public AudioClip[] pickUpSounds;
    [Range(0, 1)]
    public float pickUpSoundVolume = 0.6f;
    public AudioClip hitSound;
    [Range(0, 1)]
    public float hitVolume = 0.6f;


    AudioSource generalAudioSource;
    
    void Awake() {

        generalAudioSource = transform.Find("Audio").GetComponent<AudioSource>();
    }

    public void playPickUp() {
        AudioSource.PlayClipAtPoint(pickUpSounds[Random.Range(0, pickUpSounds.Length)], Camera.main.transform.position, pickUpSoundVolume);
    }

    public void playHit() {

        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, hitVolume);

    }
}
