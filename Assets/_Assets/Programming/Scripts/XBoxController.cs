using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class XBoxController : MonoBehaviour {

    // Will need to set dynamically here!!!
    public PlayerIndex playerIndex;

    GamePadState state;

    public AnimationCurve hold;
    public AnimationCurve press;
    public AnimationCurve good;
    public AnimationCurve bad;
    public AnimationCurve death;


    bool held;



    public void PressVibration() {
        held = true;
        StartCoroutine(CurvedVibration(press));
        StartCoroutine(CurvedVibrationContinuous(hold));

    }

    public void ReleaseVibration() {
        held = false;

    }

    public void GoodPickupVibration() {
        StartCoroutine(CurvedVibration(good));

    }

    public void BadPickupVibration() {
        StartCoroutine(CurvedVibration(bad));

    }

    public IEnumerator DeathVibration() {
        yield return StartCoroutine(CurvedVibration(death));

    }

    public void StopVibration() {
        GamePad.SetVibration(playerIndex, 0, 0);

    }



    public IEnumerator CurvedVibration(AnimationCurve curve) {
        float t = 0;

        float curveLength = curve.keys[curve.keys.Length - 1].time;

        while (t < curveLength) {
            GamePad.SetVibration(playerIndex, curve.Evaluate(t), curve.Evaluate(t));        
            t += Time.deltaTime;
            yield return null;
        }
        GamePad.SetVibration(playerIndex, 0, 0);
        yield return null;
    }

    public IEnumerator CurvedVibrationContinuous(AnimationCurve curve) {
        float t = 0;

        float curveLength = curve.keys[curve.keys.Length - 1].time;
        while (held) { 
            while (t < curveLength && held) {

                GamePad.SetVibration(playerIndex, curve.Evaluate(t), curve.Evaluate(t));        
                t += Time.deltaTime;
                yield return null;
            }

            GamePad.SetVibration(playerIndex, curve.Evaluate(curveLength), curve.Evaluate(curveLength));
            yield return null;
        }

        GamePad.SetVibration(playerIndex, 0, 0);
        yield return null;

    }


}
