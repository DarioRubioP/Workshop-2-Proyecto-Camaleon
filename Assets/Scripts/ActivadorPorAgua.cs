using UnityEngine;

public class ActivadorPorAgua : MonoBehaviour
{
    [SerializeField] private GameObject textoOculto; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Agua"))
        {
            textoOculto.SetActive(true); 
            Debug.Log("�Texto activado por contacto con agua!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Agua"))
        {
            textoOculto.SetActive(false);

        }
    }
}