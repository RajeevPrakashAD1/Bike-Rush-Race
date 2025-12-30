using UnityEngine;

public class CameraBendRollController : MonoBehaviour
{
    [Header("Bend Input")]
    [Tooltip("Negative = left bend, Positive = right bend")]
    [Range(-1f, 1f)]
    [SerializeField] private float bendStrength;

    [Header("Tuning")]
    [Tooltip("Maximum roll angle in degrees")]
    [SerializeField] private float maxRollAngle = 8f;

    [Tooltip("How fast the camera leans in/out")]
    [SerializeField] private float smoothSpeed = 3f;

    public float CurrentBend => bendStrength;

    private float currentRoll;
    private Quaternion baseLocalRotation;

    private void Awake()
    {
        baseLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        float targetRoll = bendStrength * maxRollAngle;

        // Frame-rate independent smoothing
        float t = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime);
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, t);

        transform.localRotation =
            baseLocalRotation *
            Quaternion.Euler(0f, 0f, -currentRoll);
    }

    // =========================
    // PUBLIC API
    // =========================
    public void SetBend(float bend)
    {
        bendStrength = Mathf.Clamp(bend, -1f, 1f);
    }

    public void ClearBend()
    {
        bendStrength = 0f;
    }
}