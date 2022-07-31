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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveInAction)
        {
            if (spawnDelay <= (Time.time - lastSpawn))
            {
                if (nbEnemySpawned < maxEnemies)
                {
                    int randomEnemyIndex = Random.Range(0, enemies.Length);
                    Instantiate(enemies[randomEnemyIndex], enemiesSpawnPoint.transform.position, Quaternion.identity);
                    nbEnemySpawned++;
                }
                else
                {
                    waveInAction = false;
                    nbEnemySpawned = 0;
                    GameObject protal = Instantiate(portalToTown, startWavePoint.transform.position, Quaternion.identity);
                    protal.name = "PortalToTown";
                }

                lastSpawn = Time.time;
            }
        }
    }

    public void StartWave()
    {
        wave++;
        maxEnemies += 5;
        waveInAction = true;
    }
}
