using UnityEngine;

public class ActivadorPorAgua : MonoBehaviour
{
    [SerializeField] private GameObject textoOculto; // Referencia al objeto de texto desactivado

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si el objeto que colisiona tiene el tag "Agua"
        if (other.CompareTag("Agua"))
        {
            textoOculto.SetActive(true); // Activamos el texto
            Debug.Log("�Texto activado por contacto con agua!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Opcional: Desactivar el texto cuando el agua ya no est� en contacto
        if (other.CompareTag("Agua"))
        {
            textoOculto.SetActive(false);

        }
    }
}