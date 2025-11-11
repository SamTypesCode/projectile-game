using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    [Header("Cannon Reference")]
    public Cannon cannon;

    [Header("Fire Settings (Inspector)")]
    [Tooltip("Angle in degrees to rotate the cannon to before firing")]
    public float targetAngle = 45f;

    [Tooltip("Velocity of the cannonball")]
    public float targetVelocity = 10f;

    [Header("Input Key")]
    public KeyCode shootKey = KeyCode.Space;

    void Update()
    {
        if (cannon != null && Input.GetKeyDown(shootKey))
        {
            cannon.FireAtAngle(targetAngle, targetVelocity);
        }
    }
}
