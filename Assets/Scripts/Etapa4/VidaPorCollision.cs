using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidaPorCollision : MonoBehaviour
{
    public int vidaTotal = 3;
    public TextMeshProUGUI textoVida;

    //public HealthBar healthBar;

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

            //healthBar.UpdateHealth(vidaTotal, 3);
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