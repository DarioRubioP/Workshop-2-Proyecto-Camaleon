using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Prefab to spawn")]
    public GameObject prefabToSpawn;
    [Header("Spawn To Player Y")]
    public Transform player;
    [Tooltip("Array of spawn points")]
    public Transform[] spawnPoints; 
    [Tooltip("Delay between spawns in seconds")]
    public float spawnDelay = 3f;
    [SerializeField] private bool _followsYAxis;

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
            SpawnPrefabAtPlayer();
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

    void SpawnPrefabAtPlayer()
    {
        int index = 4;
        Transform spawner = spawnPoints[index];
        Vector2 desiredPosition = _followsYAxis ? new Vector2(spawner.position.x, player.position.y) : new Vector2(player.position.x, spawner.position.y);
        spawner.position = desiredPosition;
    }
}