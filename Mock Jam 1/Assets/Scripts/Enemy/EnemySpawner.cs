using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState
    {
        active,
        destroyed
    }
    public SpawnState currentState;
    [SerializeField] public GameObject basicEnemyPrefab;
    [SerializeField] public GameObject[] enemySpawnpoints;
    [SerializeField] private int spawnDelay;
    [SerializeField] private int tempSpawnLimit;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int currentSpawnpointIndex;
    private bool isSpawning = false;

    void Start()
    {

    }

    void Update()
    {
        if(checkSpawn())
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        isSpawning = true;
        if(currentSpawnpointIndex == enemySpawnpoints.Length)
        {
            currentSpawnpointIndex = 0;
        }

        GameObject NewEnemy = Instantiate
        (
            basicEnemyPrefab,
            enemySpawnpoints[currentSpawnpointIndex].transform.position,
            Quaternion.identity
        );

        activeEnemies.Add(NewEnemy);
        currentSpawnpointIndex++;

        yield return new WaitForSeconds(spawnDelay);
        isSpawning = false;
    }

    bool checkSpawn()
    {
        if(activeEnemies.Count < tempSpawnLimit && !isSpawning)
        {
            return true;
        }
        else return false;
    }

}
