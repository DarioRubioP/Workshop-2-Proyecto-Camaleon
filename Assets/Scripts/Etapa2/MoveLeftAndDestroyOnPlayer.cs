using UnityEngine;

public class MoveLeftAndDestroyOnPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float speed = 5f;

    [Header("Collision Settings")]
    [Tooltip("Layer del jugador (opcional)")]
    public LayerMask playerLayer; // Asigna la layer del Player en el Inspector

    private Rigidbody2D rb;
    private Vector2 movementDirection = Vector2.left;
    public bool constantSpeed = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ConfigurePhysics();
    }

    private void ConfigurePhysics()
    {
        rb.gravityScale = 0; // Sin gravedad
        rb.freezeRotation = true; // Bloquea rotación
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // Movimiento suave
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Detección precisa
    }

    private void FixedUpdate()
    {
        // Movimiento basado en fisica
        rb.linearVelocity = new Vector2(-moveSpeed, 0);
        MoveObject();
    }

    private void MoveObject()
    {
        if (constantSpeed)
        {
            // Mantiene velocidad constante ignorando fuerzas externas
            rb.linearVelocity = movementDirection * speed;
        }
        else
        {
            // Aplica fuerza para movimiento más físico
            rb.AddForce(movementDirection * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisione con: " + collision.gameObject.name);
        // Verifica por tag
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyPrefab();
        }

        
        // OPCIONAL: Verificaci�n adicional por layer (m�s eficiente)
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            DestroyPrefab();
        }
        
    }

    private void DestroyPrefab()
    {
        Debug.Log("Colision fisica detectada con Player - Destruyendo objeto");
        Destroy(gameObject);

        // Opcional: Efectos de destrucci�n
        // if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }
}