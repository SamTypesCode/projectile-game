using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    [Header("Setup")]
    public Transform barrelTip;
    public GameObject cannonballPrefab;
    public float shootForce = 10f;

    [Header("Audio")]
    public AudioSource fuseAudio;
    public AudioSource shootAudio;
    public AudioClip fuseClip;
    public AudioClip shootClip;

    [Header("Animation")]
    public Animator barrelAnimator;
    public string shootAnimationName = "Shoot";
    public float shootDelaySeconds = 0.9f;

    private bool isShooting = false;

    public void Shoot()
    {
        if (!isShooting)
            StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        if (barrelAnimator != null)
            barrelAnimator.Play(shootAnimationName, -1, 0f);

        float elapsed = 0f;
        while (elapsed < shootDelaySeconds)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (cannonballPrefab != null && barrelTip != null)
        {
            GameObject cannonball = Instantiate(cannonballPrefab, barrelTip.position, barrelTip.rotation);
            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = barrelTip.right * shootForce;
        }

        isShooting = false;
    }

    public void PlayFuseSound()
    {
        if (fuseAudio != null && fuseClip != null)
            fuseAudio.PlayOneShot(fuseClip);
    }

    public void PlayShootSound()
    {
        if (shootAudio != null && shootClip != null)
            shootAudio.PlayOneShot(shootClip);
    }
}
