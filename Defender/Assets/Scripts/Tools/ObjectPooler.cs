using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<Enemy> enemyPrefabs;
    public int poolSizePerType = 100;

    private Dictionary<Enemy, List<Enemy>> enemyPool;

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
        foreach (Enemy enemyPrefab in enemyPrefabs)
        {
            enemyPool[enemyPrefab] = new List<Enemy>();
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
        foreach (Enemy enemy in enemyPool[enemyType])
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.Activate();
                return enemy;
            }
        }

        // If no inactive enemy of this type is found, expand the pool
        Enemy newEnemy = Instantiate(enemyType, transform);
        newEnemy.Initialize();
        enemyPool[enemyType].Add(newEnemy);
        return newEnemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.Deactivate();
    }
}
