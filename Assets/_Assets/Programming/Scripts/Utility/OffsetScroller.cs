#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {
    
    
    // TODO: More options 
    public float scrollmodifer = 1f;
    float scrollSpeed;
    private Vector2 savedOffset;

    Renderer rd;



    void Start () {
        rd = transform.GetComponent<Renderer>();
        savedOffset = rd.sharedMaterial.GetTextureOffset ("_MainTex");
    }

    void Update () {

        // References an active SceneController

        scrollSpeed = SceneController.instance.GetScrollSpeed() * scrollmodifer;

        float x = Mathf.Repeat(SceneController.instance.GetGameTime() * scrollSpeed * 0.01f, 1);
        Vector2 offset = new Vector2 (x, savedOffset.y);
        rd.sharedMaterial.SetTextureOffset ("_MainTex", offset);
    }

    void OnDisable () {
        //rd.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
    }

}