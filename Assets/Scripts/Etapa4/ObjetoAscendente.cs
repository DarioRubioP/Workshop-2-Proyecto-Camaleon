using UnityEngine;

public class ObjetoAscendente : MonoBehaviour
{
    public float velocidadSubida = 2f;

    void Update()
    {
        transform.position += Vector3.up * velocidadSubida * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
