using UnityEngine;

public class CameraWobbleShake : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerWobbleController wobble;
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Position Shake")]
    [SerializeField] private float maxPositionShake = 0.06f;

    [Header("Rotation Shake (Roll)")]
    [SerializeField] private float maxRotationShake = 2.5f;

    [Header("Frequency")]
    [SerializeField] private float baseFrequency = 12f;

    [Header("Smoothing")]
    [SerializeField] private float smooth = 12f;

    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;

    private float noiseTime;

    private void Awake()
    {
        initialLocalPos = transform.localPosition;
        initialLocalRot = transform.localRotation;
    }

    private void LateUpdate()
    {
        
        float wobbleAmount = wobble.WobbleAmount;

        // =========================
        // EARLY EXIT (IMPORTANT)
        // =========================
        if (wobbleAmount <= 0.001f)
        {
            ReturnToRest();
            return;
        }
        Debug.Log("wobbling camera shake");
        float speed01 = speedController.Speed01;

        // =========================
        // CACHE SPEED MULTIPLIERS
        // =========================
        float freqMul = Mathf.Lerp(0.6f, 1.4f, speed01);
        float intensityMul = Mathf.Lerp(0.6f, 1.2f, speed01);

        float intensity = wobbleAmount * intensityMul;

        noiseTime += Time.deltaTime * baseFrequency * freqMul;

        // =========================
        // PERLIN NOISE (ONCE)
        // =========================
        float noiseX = Mathf.PerlinNoise(noiseTime, 0f) - 0.5f;
        float noiseY = Mathf.PerlinNoise(0f, noiseTime) - 0.5f;

        // =========================
        // POSITION SHAKE (SUBTLE)
        // =========================
        Vector3 posOffset = new Vector3(
            noiseX * maxPositionShake * intensity,
            noiseY * maxPositionShake * intensity,
            0f
        );

        // =========================
        // ROTATION SHAKE (ROLL ONLY)
        // =========================
        float roll =
            noiseX * maxRotationShake * intensity;

        Quaternion rotOffset = Quaternion.Euler(0f, 0f, roll);

        // =========================
        // APPLY SMOOTHLY
        // =========================
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            initialLocalPos + posOffset,
            smooth * Time.deltaTime
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            initialLocalRot * rotOffset,
            smooth * Time.deltaTime
        );
    }

    private void ReturnToRest()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            initialLocalPos,
            smooth * Time.deltaTime
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            initialLocalRot,
            smooth * Time.deltaTime
        );
    }
}
