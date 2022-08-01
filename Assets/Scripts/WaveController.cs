using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public bool waveInAction = false;
    private int wave = 0;

    public GameObject[] enemies;
    private int maxEnemies = 0;
    private int nbEnemySpawned = 0;

    public GameObject enemiesSpawnPoint;

    private float lastSpawn = 0;
    public float spawnDelay = 4;

    public GameObject portalToTown;
    public GameObject startWavePoint;

    public GameObject[] enemiesSpawned;

    public GameObject upgradeUI;

    // Start is called before the first frame update
    void Start()
    {
        enemiesSpawned = new GameObject[0];
        portalToTown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveInAction)
        {
            if (enemiesSpawned.Length == 0)
            {
                enemiesSpawned = new GameObject[maxEnemies];
            }

            if (spawnDelay <= (Time.time - lastSpawn))
            {
                if (nbEnemySpawned < maxEnemies)
                {
                    int randomEnemyIndex = Random.Range(0, enemies.Length);
                    enemiesSpawned[nbEnemySpawned] = Instantiate(enemies[randomEnemyIndex], enemiesSpawnPoint.transform.position, Quaternion.identity);
                    nbEnemySpawned++;
                }
                else
                {
                    if (!CheckEnemiesExistence(enemiesSpawned))
                    {
                        waveInAction = false;
                        nbEnemySpawned = 0;
                        portalToTown.SetActive(true);
                        enemiesSpawned = new GameObject[0];
                        upgradeUI.SetActive(true);
                    }
                }

                lastSpawn = Time.time;
            }
        }
    }

    private bool CheckEnemiesExistence(GameObject[] enemiesSpawned)
    {
        bool isEnemyExist = false;

        for (int index = 0; index < enemiesSpawned.Length; index++)
        {
            if (enemiesSpawned[index] != null)
            {
                isEnemyExist = true;
            }
        }
        return isEnemyExist;
    }

    public void StartWave()
    {
        wave++;
        maxEnemies += 5;
        waveInAction = true;
    }
}
