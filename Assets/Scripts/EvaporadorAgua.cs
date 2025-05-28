using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class EvaporadorAgua : MonoBehaviour
{
    [Header("Configuraci�n Rotaci�n")]
    public float rotationSpeed = 180f;
    public float inertiaDecay = 30f;
    public float gravityPull = 20f;

    [Header("Configuraci�n Rayo")]
    public float rayLength = 5f;
    public LayerMask waterLayer;

    [Header("Configuraci�n UI")]
    public TextMeshProUGUI textoEvaporacion;
    public Button botonIzquierda;
    public Button botonDerecha;

    [Header("Configuraci�n Escena")]
    public string nextSceneName = "NextScene";

    private int counter = 0;
    private bool isEvaporating = false;
    private Coroutine evaporationCoroutine;

    private float currentRotationSpeed = 0f;
    private bool rotandoIzquierda = false;
    private bool rotandoDerecha = false;

    public AudioSource audioSource;

    void Start()
    {
        //Asignar eventos a botones UI
        botonIzquierda.onClick.AddListener(() => BotonUIRotar(-1));
        botonDerecha.onClick.AddListener(() => BotonUIRotar(1));

        //Tambi�n detectar cu�ndo se suelta el bot�n (requiere m�todos p�blicos)
        botonIzquierda.gameObject.AddComponent<BotonUIHold>().Init(() => rotandoIzquierda = true, () => rotandoIzquierda = false);
        botonDerecha.gameObject.AddComponent<BotonUIHold>().Init(() => rotandoDerecha = true, () => rotandoDerecha = false);
    }

    void Update()
    {
        HandleRotation();
        CheckWaterDetection();
    }

    void HandleRotation()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.A) || rotandoIzquierda) input += 1f;
        if (Input.GetKey(KeyCode.D) || rotandoDerecha) input -= 1f;

        //Si hay input, ajustamos la velocidad
        if (input != 0)
        {
            currentRotationSpeed = input * rotationSpeed;
        }
        else
        {
            //Aplicar ca�da por peso
            currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, -gravityPull, inertiaDecay * Time.deltaTime);
        }

        transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);
    }

    void CheckWaterDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayLength, waterLayer);
        Debug.DrawRay(transform.position, transform.up * rayLength, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Agua"))
        {
            if (!isEvaporating)
                StartEvaporation();
        }
        else
        {
            if (isEvaporating)
                StopEvaporation();
        }
    }

    void StartEvaporation()
    {
        isEvaporating = true;
        textoEvaporacion.gameObject.SetActive(true);
        evaporationCoroutine = StartCoroutine(EvaporationProcess());

        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
            
    }

    void StopEvaporation()
    {
        isEvaporating = false;
        textoEvaporacion.gameObject.SetActive(false);

        if (evaporationCoroutine != null)
        {
            StopCoroutine(evaporationCoroutine);
        }
    }

    IEnumerator EvaporationProcess()
    {
        while (counter < 10)
        {
            counter++;
            textoEvaporacion.text = $"Evaporando agua {counter}";
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(nextSceneName);
    }

    //M�todo para ser llamado por bot�n UI
    public void BotonUIRotar(int direccion)
    {
        if (direccion < 0) rotandoIzquierda = true;
        if (direccion > 0) rotandoDerecha = true;
    }
}
