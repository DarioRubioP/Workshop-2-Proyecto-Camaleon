using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class BotonComboManager : MonoBehaviour
{
    [Header("Contador")]
    public float contador = 0f;
    public float velocidadSubida = 10f;
    public float velocidadBajada = 5f;
    public float maxContador = 100f;
    private bool progresoCompletado = false;

    [Header("Tecla actual")]
    public string teclaActual = "W";
    private float tiempoCambio = 2f;
    private float tiempoRestanteCambio;

    [Header("Texto de instrucciones")]
    public TextMeshProUGUI textoIndicador;
    public TextMeshProUGUI textoProgreso; 

    [Header("Texto al completar progreso")]
    public GameObject textoFinal;

    [Header("Objeto que tiembla")]
    public Transform objetoTemblor;
    public float intensidadTemblor = 0.1f;

    [Header("Prefab a instanciar")]
    public GameObject prefab;
    public Transform puntoInstancia;
    public Transform puntoInstancia2;
    public Transform puntoInstancia3;
    
    [Header("Escena siguiente")]
    public string nextSceneName = "NextScene";

    private bool cuentaRegresivaIniciada = false;

    public AudioSource audioSource;

    void Start()
    {
        tiempoRestanteCambio = tiempoCambio;
        ElegirTeclaNueva();

        if (textoFinal != null)
            textoFinal.SetActive(false);
    }

    void Update()
    {
        if (!progresoCompletado)
        {
            tiempoRestanteCambio -= Time.deltaTime;
            if (tiempoRestanteCambio <= 0f)
            {
                ElegirTeclaNueva();
                tiempoRestanteCambio = tiempoCambio;
            }

            if (textoIndicador != null)
                textoIndicador.text = "Presiona botón " + teclaActual;

            if (Input.GetKeyDown(teclaActual.ToLower()))
            {
                contador += velocidadSubida * Time.deltaTime * 60f;
                contador = Mathf.Clamp(contador, 0f, maxContador);

                if (objetoTemblor != null)
                {
                    objetoTemblor.position = Vector3.up * 4;

                    StartCoroutine(Temblar());
                }
                    

                if (prefab != null && puntoInstancia != null)
                    Instantiate(prefab, puntoInstancia.position, Quaternion.identity);
                    Instantiate(prefab, puntoInstancia2.position, Quaternion.identity);
                    Instantiate(prefab, puntoInstancia3.position, Quaternion.identity);
            }
            else
            {
                contador -= velocidadBajada * Time.deltaTime;
                contador = Mathf.Clamp(contador, 0f, maxContador);
            }

            if (textoProgreso != null)
                textoProgreso.text = "Progreso = " + Mathf.FloorToInt(contador);

            if (contador >= maxContador)
            {
                progresoCompletado = true;

                if (textoFinal != null)
                    textoFinal.SetActive(true);
            }
        }

        if (textoFinal != null && textoFinal.activeSelf && !cuentaRegresivaIniciada)
        {
            cuentaRegresivaIniciada = true;
            StartCoroutine(CambiarDeEscena());
        }
    }

    void ElegirTeclaNueva()
    {
        string[] teclas = { "W", "A", "S", "D" };
        teclaActual = teclas[Random.Range(0, teclas.Length)];
    }

    IEnumerator Temblar()
    {
        Vector3 posicionOriginal = objetoTemblor.position;
        float duracion = 0.2f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float offsetX = Mathf.Sin(tiempo * 50f) * intensidadTemblor;
            objetoTemblor.position = new Vector3(posicionOriginal.x + offsetX, posicionOriginal.y, posicionOriginal.z);
            tiempo += Time.deltaTime;
            yield return null;
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        objetoTemblor.position = posicionOriginal;
    }

    IEnumerator CambiarDeEscena()
    {
        Debug.Log("Cambio de escena en 3 segundos...");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneName);
    }
}
