using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] ProgressMetter progressBar;

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
    public float tiempoMaximo;

    //public ProgressMetter progressBar;

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
            progressBar.FillProgressBar(tiempo, tiempoMaximo);
            if (contadorTexto != null)
                contadorTexto.text = "Contador: " + tiempoInt;

            if (tiempo >= tiempoMaximo)
            {
                yaActivadoPorContador = true;
                if (textoFinal != null)
                    textoFinal.gameObject.SetActive(true);

                StartCoroutine(CambiarEscenaDespuesDeTiempo(3f));
            }

            //progressBar.FillProgressBar(tiempo, 30);
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