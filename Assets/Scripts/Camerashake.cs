using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Header("=== Shake ===")]
    public float shakeMagnitude = 0.05f;
    public float damping = 10f;        // how fast it normalize

    private Vector3 initialLocalPos;
    private Vector3 shakeOffset;

    void Awake()
    {
        Instance = this;
        initialLocalPos = transform.localPosition;
    }

    void Update()
    {
        // lerp shake offset back to zero
        shakeOffset = Vector3.Lerp(shakeOffset, Vector3.zero, damping * Time.deltaTime);
        transform.localPosition = initialLocalPos + shakeOffset;
    }

    public void Shake()
    {
        // random offset in x and y only, z would push camera into walls
        shakeOffset = new Vector3(
            Random.Range(-shakeMagnitude, shakeMagnitude),
            Random.Range(-shakeMagnitude, shakeMagnitude),
            0f
        );
    }
}