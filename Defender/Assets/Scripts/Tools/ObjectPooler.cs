using System.Collections.Generic;
using UnityEngine;

// This class manages the object pool
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<Enemy> enemyPrefabs; // List of enemy prefabs to be pooled
    public int poolSizePerType = 100; // Number of instances to create per enemy type

    private Dictionary<Enemy, List<Enemy>> enemyPool; // Dictionary to store pooled enemies

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

        enemyPool = new Dictionary<Enemy, List<Enemy>>();

        // Create and populate the object pool for each enemy type
        foreach (Enemy enemyPrefab in enemyPrefabs)
        {
            enemyPool[enemyPrefab] = new List<Enemy>();

            // Instantiate and initialize the specified number of enemy instances
            for (int i = 0; i < poolSizePerType; i++)
            {
                Enemy newEnemy = Instantiate(enemyPrefab, transform);
                newEnemy.Initialize();
                newEnemy.Deactivate();
                enemyPool[enemyPrefab].Add(newEnemy);
            }
        }
    }

    public Enemy GetEnemy(Enemy enemyType)
    {
        // Retrieve an inactive enemy from the pool of the specified enemy type
        foreach (Enemy enemy in enemyPool[enemyType])
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.Activate();
                return enemy;
            }
        }

        // If no inactive enemy of this type is found, expand the pool by creating a new instance
        Enemy newEnemy = Instantiate(enemyType, transform);
        newEnemy.Initialize();
        enemyPool[enemyType].Add(newEnemy);
        return newEnemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        // Deactivate and return the enemy to the pool
        enemy.Deactivate();
    }
}
