using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathAndTimeManager : MonoBehaviour
{
    [Header("Detectar desactivación de objeto")]
    public GameObject objetoAObservar;
    private bool isDeath = false;
    private bool yaDetectado = false;

    [Header("Cambio de escena por tiempo")]
    public GameObject textoAdvertencia;
    private float tiempoTotal = 0f;
    private bool yaMostrado = false;
    private bool cuentaRegresivaIniciada = false;

    [Header("Destruir objeto al mostrar texto")]
    public GameObject objetoADestruir;

    [Header("Nombre de la siguiente escena")]
    public string nextSceneName = "NextScene";

    void Update()
    {
        tiempoTotal += Time.deltaTime;

        if (tiempoTotal >= 30f && !yaMostrado)
        {
            textoAdvertencia.SetActive(true);
            yaMostrado = true;

            if (objetoADestruir != null)
            {
                Destroy(objetoADestruir);
                Debug.Log("Objeto destruido al activarse el texto.");
            }
        }

        if (textoAdvertencia.activeSelf && !cuentaRegresivaIniciada)
        {
            cuentaRegresivaIniciada = true;
            StartCoroutine(CambiarDeEscena());
        }

        if (!yaDetectado && objetoAObservar != null && !objetoAObservar.activeInHierarchy)
        {
            isDeath = true;
            yaDetectado = true;
            StartCoroutine(ReiniciarEscena());
        }
    }

    IEnumerator ReiniciarEscena()
    {
        Debug.Log("Muerte detectada. Reiniciando en 3 segundos...");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CambiarDeEscena()
    {
        Debug.Log("Texto activo. Cambiando a la siguiente escena en 3 segundos...");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneName);
    }
}
