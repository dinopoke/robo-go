using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Variables", menuName = "Variables/Game", order = 5)]

public class GameVariablesScriptableObject : ScriptableObject {

    [Header("General Settings")]
    public float scrollSpeed;

}
