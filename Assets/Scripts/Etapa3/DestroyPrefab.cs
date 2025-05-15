using UnityEngine;

public class DestruirDespuesDeTiempo : MonoBehaviour
{
    public float tiempoDeVida = 5f; // Tiempo en segundos antes de destruir el objeto

    void Start()
    {
        // Llama al método DestruirObjeto después del tiempo especificado
        Invoke("DestruirObjeto", tiempoDeVida);
    }

    void DestruirObjeto()
    {
        // Destruye este objeto de juego
        Destroy(gameObject);
    }
}