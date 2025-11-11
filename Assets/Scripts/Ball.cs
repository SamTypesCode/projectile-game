using UnityEngine;

public class Ball : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Animator animator;       
    private bool hasLanded = false;

    [Header("Destroy Thresholds")]
    public float horizontalThreshold = 0.1f;
    public float verticalThreshold = 0.1f;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.linearDamping = 0.5f;
        rb.angularDamping = 2f;

        if (animator != null)
            animator.SetBool("Flying", true);
    }

    void Update()
    {
        if (mainCamera == null) return;

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPos.x < -horizontalThreshold || viewportPos.x > 1f + horizontalThreshold ||
            viewportPos.y < -verticalThreshold)
        {
            Destroy(gameObject);
            return;
        }

        if (rb != null && !hasLanded)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLanded && collision.collider.CompareTag("Ground"))
        {
            hasLanded = true;

            if (animator != null)
                animator.SetBool("Flying", false);
        }
    }
}
