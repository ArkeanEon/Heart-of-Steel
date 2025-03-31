using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float minSpawnRate = 2f;
    [SerializeField] float maxSpawnRate = 8f;
    bool isSpawning = false;

    /*private void OnEnable() {
        EventManager.onStartGame += StartToSpawn;
    }

    private void OnDisable() {
        StopToSpawn();
        EventManager.onStartGame -= StartToSpawn;
    }*/
    void Start() {
        StartToSpawn();
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        CaculateNextSpawn();
    }

    void StartToSpawn() {
        isSpawning = true;
        CaculateNextSpawn();
        //InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }

    void StopToSpawn() {
        isSpawning = false;
        CancelInvoke(nameof(SpawnEnemy));
    }

    void CaculateNextSpawn() {
        if(isSpawning) {
            float spawnDelay = Random.Range(minSpawnRate, maxSpawnRate);
            Invoke(nameof(SpawnEnemy), spawnDelay);
        }
    }
}
