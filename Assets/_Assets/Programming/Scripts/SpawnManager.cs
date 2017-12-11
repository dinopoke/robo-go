using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class SpawnManager : MonoBehaviour {

    public static SpawnManager instance;

    // Pickups
    public GameObject player1Pickup;
    public GameObject player2Pickup;

    public float spawnXLocation = 15f;

    bool spawning;

    enum patternType {line, wall, curve, custom, sizeOfEnum}
    public List<GameObject> customPatterns;

    public float timeBetweenSpawns = 0.5f;

    // Line variables
    public Vector2 lineLength;
    public int maxLineLength  = 15;


    public float gap = 0.5f;

    // Wall variables
    public Vector2 wallSize;

    // Curve variables
    public Vector2 curveLength;

    public Vector2 curveAmplitude;
    public Vector2 curveFrequency;

    public float curveGap = 0.1f;



    // Other

    public float vertGap = 1f;

    public float yBoundTop = 6;
    public float yBoundBot = 0;



    float usedSpawnY;


    /// 
    /// STILL A WIP
    /// 

	void Awake() {
        instance = this;
    }


	public void Begin () {

        spawning = true;


        StartCoroutine(SpawnThings());
	}
	

    IEnumerator SpawnThings() {

        while (spawning)
        {
            // This might be inefficient?
            patternType chosenType = (patternType)Random.Range(0, (int)patternType.sizeOfEnum);
     
            yield return SpawnType(chosenType);

            yield return new WaitForSeconds(timeBetweenSpawns);

        }
        yield return null;
    }

    IEnumerator SpawnType(patternType pattern) {

        switch (pattern)
        {
        case patternType.line:

            
            int currLineLength = (int)Random.Range(lineLength.x, lineLength.y);

            StartCoroutine(SpawnLine(player1Pickup.tag ,currLineLength));
            yield return SpawnLine(player2Pickup.tag, currLineLength);

        break;

        case patternType.wall:
            yield return SpawnWall();

        break;
        case patternType.curve:
            currLineLength = (int)Random.Range(curveLength.x, curveLength.y);

            StartCoroutine(SpawnCurve(player1Pickup.tag ,currLineLength));
            yield return SpawnCurve(player2Pickup.tag, currLineLength);

        break;
        case patternType.custom:
                PatternPooler.instance.Spawn(RandomPickUp(), customPatterns[Random.Range(0, customPatterns.Count)], new Vector3(spawnXLocation, Random.Range(yBoundBot, yBoundTop),0) , Quaternion.identity);

        break;
        }
    }

    IEnumerator SpawnLine(string spawnTag, int lineLength) {

        float spawnY;

        do {
            spawnY = Random.Range(yBoundBot, yBoundTop); }
        while (Mathf.Abs(spawnY - usedSpawnY) < vertGap );

        usedSpawnY = spawnY;
          
        for(int i = 0; i < lineLength; i++) {
    
            ObjectPooler.instance.Spawn(spawnTag, new Vector3(spawnXLocation , spawnY, 0), Quaternion.identity);
            yield return new WaitForSeconds(gap);
        }
        yield return null;
    }

    IEnumerator SpawnCurve(string spawnTag, int lineLength) {

        float currentCurveAmplitude = Random.Range(curveAmplitude.x, curveAmplitude.y);
        float currentCurveFrequency = Random.Range(curveFrequency.x, curveFrequency.y);


        float spawnY;

        do {
            spawnY = Random.Range(yBoundBot, yBoundTop); }
        while (Mathf.Abs(spawnY - usedSpawnY) < vertGap );

        usedSpawnY = spawnY;

        float randomOffset = Random.value;        
        for(int i = 0; i < lineLength; i++) {

            ObjectPooler.instance.Spawn(spawnTag, new Vector3(spawnXLocation , Mathf.SmoothStep(spawnY - currentCurveAmplitude, spawnY + currentCurveAmplitude, Mathf.PingPong(( (SceneController.instance.GetGameTime()  * currentCurveFrequency)  + randomOffset), 1))), Quaternion.identity);
            yield return new WaitForSeconds(curveGap);
        }
        yield return null;
    }


    IEnumerator SpawnWall() {

        string spawnTag = PickWhichWall();

        int wallLength = (int) Random.Range(wallSize.x, wallSize.y);

        for (int i = 0; i < wallLength; i++)
        {
            for (float j = yBoundBot; j < yBoundTop; j++)
            {

                ObjectPooler.instance.Spawn(spawnTag, new Vector3(spawnXLocation, j, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(gap);
        }

        yield return null;

    }

    string PickWhichWall() {

        // Should be a weighted choice

        if(SceneController.instance.GetFurthestPlayer() == SceneController.instance.player1.name) {

            return player1Pickup.tag;
        }
        else if (SceneController.instance.GetFurthestPlayer() == SceneController.instance.player2.name) {

            return player2Pickup.tag;

        }
        else {
            return RandomPickUp().tag;
        }

    }
    
    GameObject RandomPickUp() {

        int i = Random.Range(0, 2);
        if (i == 1) {
            return player1Pickup;
        }
        else {
            return player2Pickup;
        }
    }


    public void End() {
        spawning = false;
    }

}