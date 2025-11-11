using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField angleInput;
    public TMP_InputField velocityInput;

    [Header("Cannon Reference")]
    public Cannon cannon;
    public GameObject inputCanvas;

    [Header("Input Key")]
    public KeyCode shootKey = KeyCode.Space;

    void Start()
    {

    }

    void Update()
    {
        if (cannon != null && Input.GetKeyDown(shootKey))
        {
            if (float.TryParse(angleInput.text, out float angle) &&
                float.TryParse(velocityInput.text, out float velocity))
            {
                cannon.FireAtAngle(angle, velocity);

                if (inputCanvas != null)
                    inputCanvas.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Invalid input! Please enter valid numbers.");
            }
        }
    }
}
