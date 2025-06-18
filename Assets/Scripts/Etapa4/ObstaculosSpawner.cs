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
            //SpawnToPlayer();
            yield return new WaitForSeconds(delayEntreSpawns);
        }
    }
    void SpawnToPlayer()
    {
        //El Spawner debe moverse junto al eje Y del PLayer

        int index = 4;
        Transform playerPos = puntosSpawn[index];
        Instantiate(prefabAInstanciar,new Vector3(playerPos.position.x,0,0), playerPos.rotation);
    }
}
