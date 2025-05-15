using System.Collections;
using UnityEngine;

public class ObstaculosSpawner : MonoBehaviour
{
    public GameObject prefabAInstanciar;
    public Transform[] puntosSpawn;
    public float delayEntreSpawns = 2f;

    void Start()
    {
        StartCoroutine(SpawnearInfinito());
    }

    IEnumerator SpawnearInfinito()
    {
        while (true)
        {
            Transform punto = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
            Instantiate(prefabAInstanciar, punto.position, punto.rotation);
            yield return new WaitForSeconds(delayEntreSpawns);
        }
    }
}
