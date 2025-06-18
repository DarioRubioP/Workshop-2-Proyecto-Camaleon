using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] hpBar;

    private void Start()
    {
        foreach (var hp in hpBar)
        {
            hp.fillAmount = 1;
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        hpBar[currentHealth - 1].fillAmount = 0;

        //Debug.Log("cambio ValorVida");
    }
}
