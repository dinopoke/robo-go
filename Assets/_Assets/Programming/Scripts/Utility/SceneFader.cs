using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]

public class SceneFader : MonoBehaviour{


    public float fadeSpeed = 2f; // Speed that the screen fades to and from black.

    private bool sceneStarting = false; // Whether or not the scene is still fading in.
    private bool sceneEnding = false; // Whether or not the scene is still fading in.


    void Awake (){


        // Set the texture so that it is the the size of the screen and covers it.
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.currentResolution.width * 2,Screen.currentResolution.height * 2); 
        gameObject.GetComponent<Image>().enabled = true;


    } 
      
    public IEnumerator FadeIn (){

        sceneStarting = true;

        gameObject.GetComponent<Image>().enabled = true;

        while(gameObject.GetComponent<Image>().color.a > 0.05f) {
            // Lerp the colour of the texture between itself and transparent.
            gameObject.GetComponent<Image>().color = Color.Lerp(gameObject.GetComponent<Image>().color, Color.clear, fadeSpeed * Time.deltaTime);
            yield return null;
            // If the texture is almost clear…       
        }

        gameObject.GetComponent<Image>().color = Color.clear;
        gameObject.GetComponent<Image>().enabled = false;

        // The scene is no longer starting.

        sceneStarting = false;

        yield return null;


    }
    public IEnumerator FadeOut () {

        sceneEnding = true;
        // Make sure the texture is enabled.

        gameObject.GetComponent<Image>().enabled = true;

        // Start fading towards black.

        while(gameObject.GetComponent<Image>().color.a < 0.95f) {
            gameObject.GetComponent<Image>().color = Color.Lerp(gameObject.GetComponent<Image>().color, Color.black, fadeSpeed * 0.2f * Time.deltaTime);
            yield return null;

        }

        sceneEnding = false;
        yield return null;

    }


}