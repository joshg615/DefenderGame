using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will manage spawning enemies, map events, and bosses
public class SpawnManager : MonoBehaviour
{
    // Class to handle enemy spawning
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyType; // The type of enemy to spawn
        public int enemyCount; // Number of enemies of this type to spawn in this wave
        public float enemyHealth; // Health of the enemy
    }

    // Class to handle wave spawning
    [System.Serializable]
    public class Wave
    {
        public List<EnemySpawnInfo> enemies; // List of enemies in the wave
        public float spawnInterval; // Interval between enemy spawns in the wave
    }

    // Create lists to manage wave, map events, and boss spawning
    public List<Wave> waves;

    // Other variables needed for the game
    public float waveInterval = 60.0f; // Interval between waves

    private int currentWave = 0; // Current wave
    private float nextWaveTime = 0.0f; // Time until the next wave spawns

    private void Update()
    {
        // Handle wave spawning
        if (Time.time >= nextWaveTime)
        {
            // Start spawning the wave
            SpawnWave(waves[currentWave]);

            // Move to the next wave or loop back to the first wave
            currentWave = (currentWave + 1) % waves.Count;

            // Set the time for the next wave
            nextWaveTime = Time.time + waveInterval;
        }
    }

    private void SpawnWave(Wave wave)
    {
        // Spawn enemies in the wave using a coroutine to respect the spawnInterval
        StartCoroutine(SpawnWaveCoroutine(wave));
    }

    private IEnumerator SpawnWaveCoroutine(Wave wave)
    {
        // Iterate through each enemy type in the wave
        foreach (EnemySpawnInfo enemyInfo in wave.enemies)
        {
            // Spawn the specified number of enemies for the current type
            for (int i = 0; i < enemyInfo.enemyCount; i++)
            {
                // Spawn the enemy
                SpawnEnemy(enemyInfo);

                // Wait for the spawn interval before spawning the next enemy
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }

    private void SpawnEnemy(EnemySpawnInfo enemyInfo)
    {
        // Get a pooled game object for the enemy from the object pooler
        GameObject enemyObject = ObjectPooler.Instance.GetObject(enemyInfo.enemyType);

        // Get the Health component and set the health value
        Health health = enemyObject.GetComponent<Health>();
        if (health != null)
        {
            health.SetHealth(enemyInfo.enemyHealth);
        }

        // Calculate a random position for the enemy to spawn at
        Vector3 spawnPosition = GetRandomPositionOutsideCameraView();

        // Set the enemy's position and rotation
        enemyObject.transform.position = spawnPosition;
        enemyObject.transform.rotation = Quaternion.identity;
    }

    private Vector3 GetRandomPositionOutsideCameraView()
    {
        Vector3 spawnPosition;

        do
        {
            // Generate a random position within the screen bounds
            float randomX = Random.Range(0f, 1f);
            float randomY = Random.Range(0f, 1f);
            Vector3 viewportPosition = new Vector3(randomX, randomY, 0f);

            // Convert viewport position to world position
            spawnPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
            spawnPosition.z = 0f;

            // Add an offset to the position to move it outside the screen bounds
            Vector3 offset = spawnPosition - Camera.main.transform.position;
            float offsetMagnitude = 1.1f; // Adjust this value to move the spawn position further away from the screen
            spawnPosition += offset * offsetMagnitude;
        } while (IsInCameraView(spawnPosition));

        return spawnPosition;
    }

    private bool IsInCameraView(Vector3 position)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);
        return (viewportPoint.x > 0.0f && viewportPoint.x < 1.0f && viewportPoint.y > 0.0f && viewportPoint.y < 1.0f);
    }
}
