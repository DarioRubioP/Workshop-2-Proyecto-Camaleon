using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Prefab to spawn")]
    public GameObject prefabToSpawn; // Asigna tu prefab en el Inspector
    [Tooltip("Array of spawn points")]
    public Transform[] spawnPoints; // Asigna tus spawn points en el Inspector
    [Tooltip("Delay between spawns in seconds")]
    public float spawnDelay = 3f;

    private void Start()
    {
        // Comienza la corrutina de spawn
        StartCoroutine(SpawnPrefabsWithDelay());
    }

    IEnumerator SpawnPrefabsWithDelay()
    {
        // Bucle infinito (puedes modificarlo según tus necesidades)
        while (true)
        {
            // Espera el tiempo de delay
            yield return new WaitForSeconds(spawnDelay);

            // Llama a la función de spawn
            SpawnPrefabAtRandomPoint();
        }
    }

    void SpawnPrefabAtRandomPoint()
    {
        // Verifica que haya spawn points y un prefab asignado
        if (spawnPoints.Length == 0 || prefabToSpawn == null)
        {
            Debug.LogWarning("No spawn points or prefab assigned!");
            return;
        }

        // Elige un spawn point aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedSpawn = spawnPoints[randomIndex];

        // Instancia el prefab en la posición y rotación del spawn point seleccionado
        Instantiate(prefabToSpawn, selectedSpawn.position, selectedSpawn.rotation);

        // Opcional: Mensaje de depuración
        Debug.Log("Spawned prefab at: " + selectedSpawn.name);
    }
}