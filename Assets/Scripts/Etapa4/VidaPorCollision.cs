using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidaPorCollision : MonoBehaviour
{
    public int vidaTotal = 3;
    public TextMeshProUGUI textoVida;

    void Start()
    {
        ActualizarTextoVida();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            vidaTotal--;
            ActualizarTextoVida();

            if (vidaTotal <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vida = " + vidaTotal;
        }
    }
}