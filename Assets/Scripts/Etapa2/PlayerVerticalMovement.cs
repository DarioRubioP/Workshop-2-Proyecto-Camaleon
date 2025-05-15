using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Camera Boundaries")]
    [Tooltip("Offset from screen edges")]
    public float padding = 0.5f; // Espacio entre player y borde de pantalla

    private Camera mainCamera;
    private float minYBound;
    private float maxYBound;
    private float objectHeight;

    private void Start()
    {
        mainCamera = Camera.main;
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        // Calcula el alto del objeto (asumiendo un Collider2D)
        objectHeight = GetComponent<Collider2D>().bounds.extents.y;

        // Convierte los límites de pantalla a coordenadas de mundo
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        // Establece límites considerando el tamaño del objeto
        minYBound = bottomLeft.y + objectHeight + padding;
        maxYBound = topRight.y - objectHeight - padding;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Obtiene input vertical (teclas arriba/abajo o eje vertical)
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula nueva posición
        float newY = transform.position.y + verticalInput * moveSpeed * Time.deltaTime;

        // Aplica límites
        newY = Mathf.Clamp(newY, minYBound, maxYBound);

        // Actualiza posición
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Opcional: Para visualizar los límites en el Editor
    private void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            Gizmos.color = Color.green;
            Vector3 leftBound = new Vector3(transform.position.x - 1, minYBound, 0);
            Vector3 rightBound = new Vector3(transform.position.x + 1, minYBound, 0);
            Gizmos.DrawLine(leftBound, rightBound);

            leftBound.y = maxYBound;
            rightBound.y = maxYBound;
            Gizmos.DrawLine(leftBound, rightBound);
        }
    }
}