using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton pattern
public class Singleton<T> : MonoBehaviour where T : Component
{
    // A static variable to store the instance of the singleton
    protected static T _instance;

    // A property to check if an instance of the singleton exists
    public static bool HasInstance => _instance != null;

    // A property to try to get an instance of the singleton (returns null if none exists)
    public static T TryGetInstance() => HasInstance ? _instance : null;

    // A property to get the current instance of the singleton
    public static T Current => _instance;

    // A property implementing the Singleton design pattern to get the instance of the singleton
    public static T Instance
    {
        get
        {
            // If no instance of the singleton exists, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                // If it still doesn't exist, create a new game object with the singleton component attached
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name + "_AutoCreated";
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    // On Awake, we initialize our instance. Make sure to call base.Awake() in override if you need awake
    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    // Initializes the singleton
    protected virtual void InitializeSingleton()
    {
        // If we're not playing, return (don't initialize the singleton)
        if (!Application.isPlaying)
        {
            return;
        }

        // Otherwise, set the singleton instance to this component
        _instance = this as T;
    }
}
