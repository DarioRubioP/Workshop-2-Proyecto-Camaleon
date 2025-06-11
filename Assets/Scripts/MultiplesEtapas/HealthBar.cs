using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;


    public void UpdateHealth(float currentHealth, float health)
    {
        hpBar.fillAmount = currentHealth / health;

        //Debug.Log("cambio ValorVida");
    }
}
