using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonPalpitante : MonoBehaviour
{
    public RectTransform botonTransform;
    public float frecuencia = 1f;
    public float intensidad = 0.05f;

    public AudioSource audioSource;
    public string nombreEscena = "siguiente";
    public float retardoEscena = 3f;

    private Vector3 escalaOriginal;
    private bool fuePresionado = false;

    private void Start()
    {
        escalaOriginal = botonTransform.localScale;

        GetComponent<Button>().onClick.AddListener(OnBotonPresionado);
    }

    private void Update()
    {
        if (!fuePresionado)
        {
            float escala = 1 + Mathf.Sin(Time.time * frecuencia) * intensidad;
            botonTransform.localScale = escalaOriginal * escala;
        }
    }

    private void OnBotonPresionado()
    {
        if (fuePresionado) return;
        fuePresionado = true;

        if (audioSource != null)
            audioSource.Play();

        Invoke("CambiarEscena", retardoEscena);
    }

    private void CambiarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
