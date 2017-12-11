using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Variables", menuName = "Variables/Player", order = 5)]

public class PlayerVariablesScriptableObject : ScriptableObject {
    
    [Header("Jetpack Settings")]
    [Range(0, 20)]
    public float jetpackForce = 10f;
    [Range(-2, 2)]
    public float gravity = 0.5f;
    [Range(0, 10)]
    public float jetpackSpeedCap = 3f;
   
    [Header("PickUp Settings")]
    [Range(0, 10)]
    public float goodPickupIncrease;
    [Range(-1, 0)]
    public float fuelDepletionSpeed;
    [Range(-10, 0)]
    public float badPickupDecrease;
    [Range(-10, 0)]
    public float backwardSpeedCap;

    [Range(0, 10)]
    public float shakeCooldown;

    [Header("Boundary Settings")]
    public float maxX;

    public float deathX;
    public float deathY;

    public float startX;


}