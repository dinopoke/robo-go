using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    AudioSource musicOneSource;
    AudioSource musicTwoSource;
    AudioSource soundSource;

    float musicOneDefaultVolume;
    float musicTwoDefaultVolume;
    float soundDefaultVolume;

    bool fading;

    // Use this for initialization
    void Awake () {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyObject(gameObject);
        }


        musicOneSource = transform.Find("Music One").GetComponent<AudioSource>();
        musicTwoSource = transform.Find("Music Two").GetComponent<AudioSource>();
        soundSource = transform.Find("Sound").GetComponent<AudioSource>();


        musicOneDefaultVolume = musicOneSource.volume;
        musicTwoDefaultVolume = musicTwoSource.volume;
        soundDefaultVolume = soundSource.volume;

    }


    public void PlayTrack(int trackNumber) {


        fading = false;

        AudioSource currentTrack = GetTrack(trackNumber);
        currentTrack.volume = musicOneDefaultVolume;

        currentTrack.Stop();
        currentTrack.Play();
    }

    public void StopTrack(int trackNumber) {

        GetTrack(trackNumber).Stop();

    }

    public void FadeOutTrack(int trackNumber, float duration) {
        AudioSource track = GetTrack(trackNumber);
        
        StartCoroutine(FadeOutTrackCoroutine(track, duration));
        

    }

    public void FadeInTrack(int trackNumber, float duration) {

        AudioSource track = GetTrack(trackNumber);
        
        StartCoroutine(FadeInTrackCoroutine(track, duration, GetDefaultVolume(trackNumber)));
        

    }

    public void CrossfadeTrack(int firstTrackNumber, int secondTrackNumber, float duration) {

        StartCoroutine(FadeOutTrackCoroutine(GetTrack(firstTrackNumber), duration));
        StartCoroutine(FadeInTrackCoroutine(GetTrack(secondTrackNumber), duration, GetDefaultVolume(secondTrackNumber)));     

    }

    IEnumerator FadeOutTrackCoroutine(AudioSource track, float duration) {
        fading = true;
        while(track.volume > 0 && fading) {
            track.volume -= Time.deltaTime * duration;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        track.volume = 0;
        fading = false;
        yield return null;
    }

    IEnumerator FadeInTrackCoroutine(AudioSource track, float duration, float defaultVolume) {
        fading = true;
        while(track.volume < defaultVolume  && fading) {
            track.volume += Time.deltaTime * duration;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        track.volume = defaultVolume;
        fading = false;
        yield return null;
    }

     public AudioSource GetTrack(int trackNumber) {

        switch(trackNumber) {
            case 1:
                return musicOneSource;
            case 2:
                return musicTwoSource;
            case 3:
                return soundSource;
            default:
                return null;   
        }
        
    }

    public float GetDefaultVolume(int trackNumber) {

        switch(trackNumber) {
            case 1:
                return musicOneDefaultVolume;
            case 2:
                return musicTwoDefaultVolume;
            case 3:
                return soundDefaultVolume;
            default:
                return 1f;   
        }
        
    }

}
