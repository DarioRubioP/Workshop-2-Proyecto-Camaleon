using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;


    public void UpdateHealth(float currentHealth, int health)
    {
        hpBar.fillAmount = health / currentHealth;
    }
}
