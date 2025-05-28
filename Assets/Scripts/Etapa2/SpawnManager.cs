using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Prefab to spawn")]
    public GameObject prefabToSpawn; 
    [Tooltip("Array of spawn points")]
    public Transform[] spawnPoints; 
    [Tooltip("Delay between spawns in seconds")]
    public float spawnDelay = 3f;

    private void Start()
    {
        StartCoroutine(SpawnPrefabsWithDelay());
    }

    IEnumerator SpawnPrefabsWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            SpawnPrefabAtRandomPoint();
        }
    }

    void SpawnPrefabAtRandomPoint()
    {
        if (spawnPoints.Length == 0 || prefabToSpawn == null)
        {
            Debug.LogWarning("No spawn points or prefab assigned!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedSpawn = spawnPoints[randomIndex];

        Instantiate(prefabToSpawn, selectedSpawn.position, selectedSpawn.rotation);

        Debug.Log("Spawned prefab at: " + selectedSpawn.name);
    }
}