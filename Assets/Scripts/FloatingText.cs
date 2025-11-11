using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    public float amplitude = 5f;
    public float frequency = 2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(startPos.x, newY, startPos.z), 0.5f);
    }
}
