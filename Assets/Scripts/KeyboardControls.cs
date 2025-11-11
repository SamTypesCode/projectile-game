using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    [Header("Cannon Reference")]
    public Cannon cannon;
    public Transform cannonTransform;

    [Header("Rotation Settings")]
    public float rotationSpeed = 15f;
    public float minRotation = -15f;
    public float maxRotation = 60f;

    [Header("Audio")]
    public AudioSource rotationSource;
    public AudioClip rotationClip;

    [Header("Input Keys")]
    public KeyCode rotateLeftKey = KeyCode.A;
    public KeyCode rotateRightKey = KeyCode.D;
    public KeyCode shootKey = KeyCode.Space;

    void Update()
    {
        if (cannonTransform != null)
        {
            float rotationInput = 0f;
            if (Input.GetKey(rotateLeftKey)) rotationInput += 1f;
            if (Input.GetKey(rotateRightKey)) rotationInput -= 1f;

            if (rotationInput != 0f)
            {
                float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;

                float currentZ = cannonTransform.eulerAngles.z;
                if (currentZ > 180f) currentZ -= 360f;

                float newZ = Mathf.Clamp(currentZ + rotationAmount, minRotation, maxRotation);

                if (Mathf.Abs(newZ - currentZ) > 0.001f)
                {
                    cannonTransform.rotation = Quaternion.Euler(0f, 0f, newZ);

                    if (rotationSource != null && rotationClip != null)
                    {
                        if (!rotationSource.isPlaying || rotationSource.clip != rotationClip)
                        {
                            rotationSource.clip = rotationClip;
                            rotationSource.loop = true;
                            rotationSource.Play();
                        }
                    }
                }
                else
                {
                    if (rotationSource != null && rotationSource.isPlaying)
                        rotationSource.Stop();
                }
            }
            else
            {
                if (rotationSource != null && rotationSource.isPlaying)
                    rotationSource.Stop();
            }
        }

        if (cannon != null && Input.GetKeyDown(shootKey))
        {
            cannon.Shoot();
        }
    }
}
