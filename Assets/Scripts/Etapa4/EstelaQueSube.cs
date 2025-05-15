using UnityEngine;

public class EstelaQueSube : MonoBehaviour
{
    public float velocidadSubida = 1f;

    private Camera camaraPrincipal;
    private float limiteSuperior;

    void Start()
    {
        camaraPrincipal = Camera.main;
        limiteSuperior = camaraPrincipal.transform.position.y + camaraPrincipal.orthographicSize + 1f; // Límite superior con margen
    }

    void Update()
    {
        transform.position += Vector3.up * velocidadSubida * Time.deltaTime;

        if (transform.position.y > limiteSuperior)
        {
            Destroy(gameObject);
        }
    }
}
