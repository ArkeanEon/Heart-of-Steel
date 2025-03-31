using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatZoneGenerator : MonoBehaviour
{
    //number of objects to spawn
    [SerializeField][Range(100f, 1000f)] float spawnRadius = 500f;
    [SerializeField][Range(0, 500)] int asteroidCount = 500;
    [SerializeField][Range(0, 500)] int enemyCount = 50;
    [SerializeField][Range(0, 50)] int spawnerCount = 10;

    //prefab types
    [SerializeField] List<GameObject> asteroidPrefabs;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<GameObject> spawnerPrefabs;
    [SerializeField] List<GameObject> bossPrefabs;
    [SerializeField] List<GameObject> exits;

    [SerializeField] float minScale = 2.5f;
    [SerializeField] float maxScale = 8f;
    [SerializeField] float asteroidMinDistance = 10f;
    [SerializeField] float enemyMinDistance = 10f;
    [SerializeField] float spawnerMinDistance = 10f;
    private List<Vector3> spawnedPositions;

    /*private void OnDisable() {
        EventManager.onStartGame -= StartSpawning;
    }*/

    void Start() {
        spawnedPositions = new List<Vector3>();
        StartCoroutine(SpawnObjectsCoroutine(asteroidCount, asteroidPrefabs, asteroidMinDistance));
        StartCoroutine(SpawnObjectsCoroutine(enemyCount, enemyPrefabs, enemyMinDistance));
        StartCoroutine(SpawnObjectsCoroutine(spawnerCount, spawnerPrefabs, spawnerMinDistance));
        StartCoroutine(SpawnObjectsCoroutine(1, bossPrefabs, enemyMinDistance));
        StartCoroutine(SpawnObjectsCoroutine(1, exits, spawnerMinDistance));
    }

    IEnumerator SpawnObjectsCoroutine(int objectCount, List<GameObject> prefabs, float minDistance)
    {
        int spawnedCount = 0;
        while (spawnedCount < objectCount)
        {
            Vector3 randomDirection = Random.insideUnitSphere;
            Vector3 newPosition = transform.position + randomDirection.normalized * Random.Range(0, spawnRadius);

            if (!IsPositionTooClose(newPosition, minDistance))
            {
                GameObject objectPrefab = Instantiate(prefabs[Random.Range(0, prefabs.Count)], newPosition, Quaternion.identity);
                HandleObjectSpecificSettings(objectPrefab);
                spawnedPositions.Add(newPosition);
                spawnedCount++;
            }

            yield return null; // Wait for next frame
        }
    }

    bool IsPositionTooClose(Vector3 position, float minDistance)
    {
        foreach (var spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    void HandleObjectSpecificSettings(GameObject objectPrefab)
    {
        if (objectPrefab.CompareTag("Asteroid"))
        {
            float newScale = Random.Range(minScale, maxScale);
            objectPrefab.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
