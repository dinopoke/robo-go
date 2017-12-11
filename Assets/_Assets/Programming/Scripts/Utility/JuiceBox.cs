using UnityEngine;
using System.Collections;

public class JuiceBox : MonoBehaviour {

    // YOU WANT SOME JUICE

    // This is kinda useless
    public static IEnumerator RandomDelayCr(float min, float max) {

        yield return new WaitForSeconds(Random.Range(min, max));
    }


    // ONLY WORKS ON ORTHOGONAL CAMERA
    public static void LookAt2D(Transform self, Vector3 target) {
        
        Quaternion rotation = Quaternion.LookRotation( self.TransformDirection(Vector3.forward), target - self.position);
        self.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

    }


    // Just x and y atm
    // Returns new scale to put into transform
    public static Vector3 StretchNSquash2D(Vector3 defaultScale, float distance, float stretchvar, float squashvar) {

        Vector3 newScale = defaultScale + new Vector3(-squashvar * distance, stretchvar * distance, 0);
        return newScale;

    }




}
