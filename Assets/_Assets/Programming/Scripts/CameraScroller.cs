using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour {

    public Vector2 CameraClamp;
    public float SmoothTime = 0.15f;

    public GameObject Player1;
    public GameObject Player2;

    Vector3 newPosition;

    float velocityX;
    float velocityY;

    void Start()
    {
    }
    void LateUpdate()
    {
        // Check if less than 2 players here

        newPosition = (Player1.transform.position + Player2.transform.position) / 2;

        if (newPosition.x > CameraClamp.x)
            newPosition.x = CameraClamp.x;
        else if (newPosition.x < -CameraClamp.x)
            newPosition.x = -CameraClamp.x;

        if (newPosition.y > CameraClamp.y)
            newPosition.y = CameraClamp.y;
        else if (newPosition.y < -CameraClamp.y)
            newPosition.y = -CameraClamp.y;

        newPosition.x = Mathf.SmoothDamp(transform.position.x, newPosition.x, ref velocityX, SmoothTime);
        newPosition.y = Mathf.SmoothDamp(transform.position.y, newPosition.y, ref velocityY, SmoothTime);





        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

}






















//enum CameraState {LeftToRight, RightToLeft, DownToUp, UpToDown };
    //
    //public GameObject Player1;
    //public GameObject Player2;
    //public float smoothTime = 0.25f;
    //
    //public float l2rDistance = 10.0f;
    //
    //float l2rVelocity = 0.0f;
    //
    //CameraState StageState = CameraState.LeftToRight;
    //
    //void LateUpdate()
    //{
    //    switch (StageState)
    //    {
    //    case CameraState.LeftToRight:
    //            L2RUpdate();
    //        break;
    //    }
    //}
    //
    //void L2RUpdate()
    //{
    //    if((transform.position.x + l2rDistance) < Player1.transform.position.x)
    //    {
    //        float newPosition = Mathf.SmoothDamp(transform.position.x, transform.position.x + 3, ref l2rVelocity, smoothTime);
    //        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    //    }
    //    else if((transform.position.x + l2rDistance) < Player2.transform.position.x)
    //    {
    //        float newPosition = Mathf.SmoothDamp(transform.position.x, transform.position.x + 3, ref l2rVelocity, smoothTime);
    //        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    //    }
    //}
    //
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(1, 0, 1, 0.25f);
    //    Gizmos.DrawLine(new Vector3(transform.position.x + l2rDistance, -10, 0), new Vector3(transform.position.x + l2rDistance, 10, 0));
    //}