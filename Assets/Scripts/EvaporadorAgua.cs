using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;


public class EvaporadorAgua : MonoBehaviour
{
    [Header("Configuración Rotación")]
    public float rotationSpeed = 180f;

    
    [Header("Configuración Rayo")]
    public float rayLength = 5f;
    public LayerMask waterLayer; // Capa para filtrar detección
    

    [Header("Configuración UI")]
    public TextMeshProUGUI textoEvaporacion; // Texto Legacy (UI Text tradicional)

    [Header("Configuración Escena")]
    public string nextSceneName = "NextScene";
    
    
    private int counter = 0;
    private bool isEvaporating = false;
    private Coroutine evaporationCoroutine;
    

    void Start()
    {
       
    }

    void Update()
    {
        HandleRotation();
        CheckWaterDetection();
    }

    void HandleRotation()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.A)) rotationInput = 1f;
        if (Input.GetKey(KeyCode.D)) rotationInput = -1f;

        transform.Rotate(Vector3.forward * rotationInput * rotationSpeed * Time.deltaTime);
    }

    
    void CheckWaterDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            transform.up,
            rayLength,
            waterLayer);

        Debug.DrawRay(transform.position, transform.up * rayLength, Color.red);

        // Detección de agua por TAG
        if (hit.collider != null && hit.collider.CompareTag("Agua"))
        {
            if (!isEvaporating)
            {
                StartEvaporation();
            }
        }
        else
        {
            if (isEvaporating)
            {
                StopEvaporation();
            }
        }
    }

    void StartEvaporation()
    {
        isEvaporating = true;
        textoEvaporacion.gameObject.SetActive(true);
        evaporationCoroutine = StartCoroutine(EvaporationProcess());
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


    
}