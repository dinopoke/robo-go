using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPattern: PickupPattern {

    public float rotationAmount = 1.0f;

	public override void PatternMovement () {
        transform.Rotate(new Vector3(0, 0, rotationAmount));
	}
}
