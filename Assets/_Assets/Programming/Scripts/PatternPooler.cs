using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class PatternPooler : ObjectPooler

{
    new public static PatternPooler instance;

    public const string DefaultRootPatternPoolName = "Pooled Patterns";

    // OVERRIDEN METHODS

    public GameObject GetPooledObject(GameObject pickup, GameObject pattern)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == GetPatternName(pickup, pattern))
                return pooledObjects[i];
        }

        return CreatePooledObject(pickup, pattern);

    }

    public GameObject Spawn(GameObject pickup, GameObject pattern, Vector3 position, Quaternion rotation) {

        GameObject pooledObject = GetPooledObject(pickup, pattern);

        if (pooledObject != null) {
            pooledObject.transform.position = position;
            pooledObject.transform.rotation = rotation;
            pooledObject.SetActive(true);

            ActivateChildren(pooledObject.transform);

        }

        return pooledObject;
    }

    void ActivateChildren(Transform obj) {
        foreach (Transform child in obj) {

            foreach (Transform pickup in child) {
                pickup.gameObject.SetActive(true);

            }

        }

    }

    string GetPatternName (GameObject pickup, GameObject pattern) {
        // Stringy
        // Make it better pls

        if (pickup.name == "Blue Pickup") {
            return "Blue " + pattern.name;
        }
        else if(pickup.name == "Red Pickup") {
            return "Red " + pattern.name;
        }
        else {
            return null;
        }
    }


    private GameObject CreatePooledObject(GameObject pickup, GameObject pattern)
    {
        GameObject obj = Instantiate(pattern);
        PickupPattern pp = obj.GetComponent<PickupPattern>();
        pp.PatternSetup(pickup);
        obj.name = GetPatternName(pickup, pattern);


        obj.transform.parent = GameObject.Find(rootPoolName).transform;

        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        rootPoolName = DefaultRootPatternPoolName;

        GetParentPoolObject(rootPoolName);

        pooledObjects = new List<GameObject>();

    }

    private GameObject GetParentPoolObject(string objectPoolName)
    {
        // Use the root object pool name if no name was specified
        if (string.IsNullOrEmpty(objectPoolName))
            objectPoolName = rootPoolName;

        GameObject parentObject = GameObject.Find(objectPoolName);

        // Create the parent object if necessary
        if (parentObject == null)
        {
            parentObject = new GameObject();
            parentObject.name = objectPoolName;

            // Add sub pools to the root object pool if necessary
            if (objectPoolName != rootPoolName)
                parentObject.transform.parent = GameObject.Find(rootPoolName).transform;
        }

        DontDestroyOnLoad(parentObject);
        return parentObject;
    }


}