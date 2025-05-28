using UnityEngine;

public class MoveLeftAndDestroyOnPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float speed = 5f;

    [Header("Collision Settings")]
    [Tooltip("Layer del jugador (opcional)")]
    public LayerMask playerLayer;

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
        rb.gravityScale = 0; 
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; 
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, 0);
        MoveObject();
    }

    private void MoveObject()
    {
        if (constantSpeed)
        {
            rb.linearVelocity = movementDirection * speed;
        }
        else
        {
            rb.AddForce(movementDirection * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisione con: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyPrefab();
        }

        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            DestroyPrefab();
        }
        
    }

    private void DestroyPrefab()
    {
        Debug.Log("Colision fisica detectada con Player - Destruyendo objeto");
        Destroy(gameObject);
    }
}