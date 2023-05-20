using System;
using System.Collections.Generic;
using UnityEngine;

// Serializable class representing a pool of objects
[Serializable]
public class Pool
{
    public GameObject prefab;  // The prefab of the object to be pooled
    public int size;  // The number of objects in the pool
}

// This class manages the object pool
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;  // Singleton instance of the ObjectPooler
    public List<Pool> pools;  // List of pools containing different object types
    private Dictionary<Type, Queue<GameObject>> objectPool;  // Dictionary to store object pools based on their type

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  // Set the singleton instance if it doesn't exist
        }
        else
        {
            Destroy(gameObject);  // Destroy the duplicate instance
            return;
        }

        objectPool = new Dictionary<Type, Queue<GameObject>>();

        // Initialize object pools for each pool defined in the 'pools' list
        foreach (Pool pool in pools)
        {
            // Create a queue to hold the objects of a specific type
            objectPool[pool.prefab.GetType()] = new Queue<GameObject>();

            // Instantiate and enqueue the objects in the pool
            for (int i = 0; i < pool.size; i++)
            {
                // Instantiate a new object based on the prefab and parent it to the ObjectPooler transform
                GameObject newObject = Instantiate(pool.prefab, transform);
                newObject.SetActive(false);  // Set the object as inactive initially
                objectPool[pool.prefab.GetType()].Enqueue(newObject);  // Enqueue the object in the pool
            }
        }
    }

    // Get an object from the pool based on its prefab type
    public GameObject GetObject(GameObject prefabType)
    {
        // Check if there are available objects of the requested type in the pool
        if (objectPool[prefabType.GetType()].Count > 0)
        {
            // Dequeue an object from the pool
            GameObject retrievedObject = objectPool[prefabType.GetType()].Dequeue();
            retrievedObject.SetActive(true);  // Activate the retrieved object
            return retrievedObject;
        }

        // If no objects are available in the pool, instantiate a new object
        GameObject newObject = Instantiate(prefabType, transform);
        objectPool[prefabType.GetType()].Enqueue(newObject);  // Enqueue the new object in the pool
        return newObject;
    }

    // Return an object back to the pool
    public void ReturnObject(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);  // Set the object as inactive
        objectPool[objectToReturn.GetType()].Enqueue(objectToReturn);  // Enqueue the returned object in the pool
    }
}
