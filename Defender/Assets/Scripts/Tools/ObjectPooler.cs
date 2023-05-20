using System;
using System.Collections.Generic;
using UnityEngine;

// This class manages the object pool
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<GameObject> prefabTypes; // List of prefab types to be pooled
    public int poolSizePerType = 100; // Number of instances to create per prefab type

    private Dictionary<Type, Queue<GameObject>> objectPool; // Dictionary to store pooled objects

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        objectPool = new Dictionary<Type, Queue<GameObject>>();

        // Create and populate the object pool for each prefab type
        foreach (GameObject prefabType in prefabTypes)
        {
            objectPool[prefabType.GetType()] = new Queue<GameObject>();

            // Instantiate and initialize the specified number of instances
            for (int i = 0; i < poolSizePerType; i++)
            {
                GameObject newObject = Instantiate(prefabType, transform);
                newObject.SetActive(false);
                objectPool[prefabType.GetType()].Enqueue(newObject);
            }
        }
    }

    public GameObject GetObject(GameObject prefabType)
    {
        // Check if there is an inactive object of the specified type
        if (objectPool[prefabType.GetType()].Count > 0)
        {
            GameObject retrievedObject = objectPool[prefabType.GetType()].Dequeue();
            retrievedObject.SetActive(true);
            return retrievedObject;
        }

        // If no inactive object of this type is found, expand the pool by creating a new instance
        GameObject newObject = Instantiate(prefabType, transform);
        objectPool[prefabType.GetType()].Enqueue(newObject);
        return newObject;
    }

    public void ReturnObject(GameObject objectToReturn)
    {
        // Deactivate and return the object to the pool
        objectToReturn.SetActive(false);
        objectPool[objectToReturn.GetType()].Enqueue(objectToReturn);
    }
}
