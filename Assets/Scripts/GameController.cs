using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Data;

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

    private string sceneName;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        angleInput.contentType = TMP_InputField.ContentType.Standard;
        velocityInput.contentType = TMP_InputField.ContentType.Standard;
    }

    void Update()
    {
        if (cannon != null && Input.GetKeyDown(shootKey))
        {
            float angle = EvaluateExpression(angleInput.text);
            float velocity = EvaluateExpression(velocityInput.text);

            if (!float.IsNaN(angle) && !float.IsNaN(velocity))
            {
                if (sceneName == "Level 1")
                {
                    cannon.FireAtAngle(angle, velocity);
                    if (inputCanvas != null)
                        inputCanvas.SetActive(false);
                }
                else if (sceneName == "Level 2")
                {
                    cannon.FireAtAngle(angle * 7 / 4, velocity * 4 / 3);
                    if (inputCanvas != null)
                        inputCanvas.SetActive(false);
                }
                else if (sceneName == "Level 3")
                {
                    cannon.FireAtAngle(angle*angle/8, velocity*velocity+1);
                    if (inputCanvas != null)
                        inputCanvas.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("Invalid input! Please enter valid numbers or math expressions.");
            }
        }
    }

    private float EvaluateExpression(string expression)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(expression))
                return float.NaN;

            var result = new DataTable().Compute(expression, "");
            return Convert.ToSingle(result);
        }
        catch
        {
            return float.NaN;
        }
    }
}
