using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
    // TODO: More options

    public float scrollModifier = 1f;
    float scrollSpeed;
    public float tileSizeX;

    private Vector3 startPosition;

    void Start ()
    {
        startPosition = transform.position;
    }

    void Update ()
    {
        // References an active SceneController

        scrollSpeed = SceneController.instance.GetScrollSpeed() * scrollModifier;


        float newPosition = Mathf.Repeat(SceneController.instance.GetGameTime() * scrollSpeed, tileSizeX);

        transform.position = startPosition + Vector3.left * newPosition;
    }
}