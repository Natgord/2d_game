using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnDelay = 0f;

    float lastSpawnTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (CheckSpawnCondition())
        {
            RandomSpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private bool CheckSpawnCondition()
    {
        // Check if the delay to spawn an enemy is reached
        if (spawnDelay <= (Time.time - lastSpawnTime))
        {
            return true;
        }

        return false;
    }

    private void RandomSpawnEnemy()
    {
        // Get a random number in length of possible enemies
        int enemyIndex = Random.Range(0, enemies.Length);

        // Instatiate the random enemy
        Instantiate(enemies[enemyIndex], transform);
    }

}
