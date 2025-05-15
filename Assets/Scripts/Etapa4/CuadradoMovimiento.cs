using UnityEngine;

public class CuadradoMovimiento : MonoBehaviour
{
    public float velocidadMovimiento = 5f;

    [Header("Estela")]
    public GameObject estelaPrefab;
    public float tiempoEntreEstelas = 0.1f;

    private float tiempoSiguienteEstela;

    private Camera camaraPrincipal;
    private float limiteIzquierdo, limiteDerecho;
    private float anchoMitad;

    void Start()
    {
        camaraPrincipal = Camera.main;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            anchoMitad = sr.bounds.size.x / 2f;
        else
            anchoMitad = 0.5f;

        CalcularLimitesCamara();
    }

    void Update()
    {
        MovimientoHorizontal();
        DejarEstela();
    }

    void MovimientoHorizontal()
    {
        float movimiento = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;
        Vector3 nuevaPos = transform.position + new Vector3(movimiento, 0, 0);

        nuevaPos.x = Mathf.Clamp(nuevaPos.x, limiteIzquierdo + anchoMitad, limiteDerecho - anchoMitad);
        transform.position = nuevaPos;
    }

    void CalcularLimitesCamara()
    {
        float alturaCamara = 2f * camaraPrincipal.orthographicSize;
        float anchoCamara = alturaCamara * camaraPrincipal.aspect;

        Vector3 centro = camaraPrincipal.transform.position;
        limiteIzquierdo = centro.x - (anchoCamara / 2f);
        limiteDerecho = centro.x + (anchoCamara / 2f);
    }

    void DejarEstela()
    {
        if (Time.time >= tiempoSiguienteEstela)
        {
            GameObject estela = Instantiate(estelaPrefab, transform.position, Quaternion.identity);
            estela.AddComponent<EstelaQueSube>(); // Agrega el comportamiento de subida/destrucción
            tiempoSiguienteEstela = Time.time + tiempoEntreEstelas;
        }
    }
}