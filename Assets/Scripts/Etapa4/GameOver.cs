using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Parte 1: Reinicio si objeto se desactiva")]
    public GameObject objetoAObservar;
    public TextMeshProUGUI textoAlDesactivar;

    [Header("Parte 2: Contador que llega a 30")]
    public TextMeshProUGUI contadorTexto;
    public TextMeshProUGUI textoFinal;
    public string nextSceneName = "NextScene";

    private bool yaActivadoPorDesactivacion = false;
    private bool yaActivadoPorContador = false;
    private float tiempo = 0f;

    void Start()
    {
        if (textoAlDesactivar != null) textoAlDesactivar.gameObject.SetActive(false);
        if (textoFinal != null) textoFinal.gameObject.SetActive(false);
    }

    void Update()
    {
        // Parte 1: Detectar desactivación de objeto
        if (!yaActivadoPorDesactivacion && objetoAObservar != null && !objetoAObservar.activeInHierarchy)
        {
            yaActivadoPorDesactivacion = true;
            if (textoAlDesactivar != null)
                textoAlDesactivar.gameObject.SetActive(true);

            StartCoroutine(ReiniciarDespuesDeTiempo(3f));
        }

        // Parte 2: Contador hasta 30
        if (!yaActivadoPorContador)
        {
            tiempo += Time.deltaTime;
            int tiempoInt = Mathf.FloorToInt(tiempo);
            if (contadorTexto != null)
                contadorTexto.text = "Contador: " + tiempoInt;

            if (tiempo >= 30f)
            {
                yaActivadoPorContador = true;
                if (textoFinal != null)
                    textoFinal.gameObject.SetActive(true);

                StartCoroutine(CambiarEscenaDespuesDeTiempo(3f));
            }
        }
    }

    IEnumerator ReiniciarDespuesDeTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CambiarEscenaDespuesDeTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(nextSceneName);
    }
}