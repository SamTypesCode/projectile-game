using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    [Header("Setup")]
    public Transform barrelTip;
    public GameObject cannonballPrefab;

    [Header("Audio")]
    public AudioSource fuseAudio;
    public AudioSource shootAudio;
    public AudioSource rotationAudio;
    public AudioClip fuseClip;
    public AudioClip shootClip;
    public AudioClip rotationClip;

    [Header("Animation")]
    public Animator barrelAnimator;
    public string shootAnimationName = "Shoot";

    [Header("Rotation")]
    public float rotationSpeed = 15f;
    public float minAngle = -15f;
    public float maxAngle = 75f;

    private bool isShooting = false;

    public void FireAtAngle(float targetAngle, float velocity)
    {
        targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

        if (!isShooting)
            StartCoroutine(RotateAndShoot(targetAngle, velocity));
    }

    private IEnumerator RotateAndShoot(float targetAngle, float velocity)
    {
        isShooting = true;

        if (rotationAudio != null && rotationClip != null)
        {
            rotationAudio.clip = rotationClip;
            rotationAudio.loop = true;
            rotationAudio.Play();
        }

        while (true)
        {
            float currentZ = transform.eulerAngles.z;
            if (currentZ > 180f) currentZ -= 360f;

            float step = rotationSpeed * Time.deltaTime;
            float newZ = Mathf.MoveTowards(currentZ, targetAngle, step);
            transform.rotation = Quaternion.Euler(0f, 0f, newZ);

            if (Mathf.Abs(newZ - targetAngle) < 0.01f)
                break;

            yield return null;
        }

        if (rotationAudio != null && rotationAudio.isPlaying)
            rotationAudio.Stop();

        if (barrelAnimator != null)
            barrelAnimator.Play(shootAnimationName, -1, 0f);

        if (fuseAudio != null && fuseClip != null)
            fuseAudio.PlayOneShot(fuseClip);

        yield return new WaitForSeconds(0.67f);

        if (cannonballPrefab != null && barrelTip != null)
        {
            GameObject cannonball = Instantiate(cannonballPrefab, barrelTip.position, barrelTip.rotation);
            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = barrelTip.right * velocity;
        }

        if (shootAudio != null && shootClip != null)
            shootAudio.PlayOneShot(shootClip);

        isShooting = false;
    }
}
