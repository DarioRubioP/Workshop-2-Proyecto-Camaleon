using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Camera Boundaries")]
    [Tooltip("Offset from screen edges")]
    public float padding = 0.5f;

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
        objectHeight = GetComponent<Collider2D>().bounds.extents.y;

        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        minYBound = bottomLeft.y + objectHeight + padding;
        maxYBound = topRight.y - objectHeight - padding;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");

        float newY = transform.position.y + verticalInput * moveSpeed * Time.deltaTime;

        newY = Mathf.Clamp(newY, minYBound, maxYBound);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
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