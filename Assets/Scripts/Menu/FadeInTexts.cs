using TMPro;
using UnityEngine;

public class FadeInTexts : MonoBehaviour
{
    public TextMeshProUGUI[] textos;
    public float duracion = 2f; // duración del fade in

    private void Start()
    {
        foreach (var texto in textos)
        {
            StartCoroutine(FadeIn(texto));
        }
    }

    private System.Collections.IEnumerator FadeIn(TextMeshProUGUI texto)
    {
        texto.alpha = 0f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            texto.alpha = Mathf.Clamp01(tiempo / duracion);
            yield return null;
        }
    }
}