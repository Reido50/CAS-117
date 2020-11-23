using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject generator;
    public GameObject enemyParent;

    public float spawnInterval = 4;
    public float spawnRadius = 150;
    public float spawnVariable = 1;

    public List<GameObject> enemyPrefabs;

    public int waveNum = 0;

    private float timeSinceLastWave = 0;
    private float timeSinceLastSpawn = 0;

    private bool waveActive = false;

    // Update is called once per frame
    void Update()
    {
        // Update time variables
        float deltaT = Time.deltaTime;
        timeSinceLastWave += deltaT;
        timeSinceLastSpawn += deltaT;
        
        // Determines when to spawn a set
        if(timeSinceLastWave <= 10.0 && timeSinceLastSpawn >= (spawnInterval)/(spawnVariable))
        {
            if(!waveActive)
            {
                waveActive = true;
                waveNum++;
                spawnVariable += 1f;
            }
            SpawnSet();
            timeSinceLastSpawn = 0;
        }

        if(waveActive && enemyParent.transform.childCount < 1)
        {
            waveActive = false;
            timeSinceLastWave = 0f;
        }
    }

    void SpawnSet()
    {
        Debug.Log("Spawn!");
        Vector3 generatorPoint = generator.transform.position;

        for (int i = 0; i < 2; i++)
        {
            float randomTheta = Random.Range(0, 2*Mathf.PI);

            float spawnX = generatorPoint.x + spawnRadius * Mathf.Cos(randomTheta);
            float spawnY = 22;
            float spawnZ = generatorPoint.z + spawnRadius * Mathf.Sin(randomTheta);

            Instantiate(enemyPrefabs[0], new Vector3(spawnX, spawnY, spawnZ), generator.transform.rotation, enemyParent.transform);
        }
        

        
    }
}
