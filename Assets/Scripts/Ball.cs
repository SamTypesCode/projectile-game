using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ball : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Animator animator;
    private bool hasLanded = false;

    [Header("Destroy Thresholds")]
    public float horizontalThreshold = 0.1f;
    public float verticalThreshold = 0.1f;

    private bool isInTargetTrigger = false;

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
            if (!hasLanded && !isInTargetTrigger) 
            {
                StartCoroutine(ReloadSceneAfterDelay(1f));
            }

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
        if (hasLanded) return;

        if (collision.collider.CompareTag("Target") && isInTargetTrigger)
            return;

        if (collision.collider.CompareTag("Ground") ||
            collision.collider.CompareTag("Cannon Ball") ||
            collision.collider.CompareTag("Target"))
        {
            hasLanded = true;

            if (animator != null)
                animator.SetBool("Flying", false);

            if (!isInTargetTrigger)
                StartCoroutine(ReloadSceneAfterDelay(1f));
        }
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
            isInTargetTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
            isInTargetTrigger = false;
    }
}
