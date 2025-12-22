using UnityEngine;

public class BikeLeanController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerLateralController lateralController;
    [SerializeField] private PlayerSpeedController speedController;
    [SerializeField] private GameplayInputHandler input;

    [Header("Lean Settings")]
    [SerializeField] private Transform bikeVisual;
    [SerializeField] private float maxLeanAngle = 25f;
    [SerializeField] private float leanSmooth = 6f;

    private float currentLean;

    private void Update()
    {
        // Motion-based lean (primary)
        float velocityLean =
            -lateralController.LateralVelocity * 0.4f;

        // Input-based lean (secondary, adds struggle feel)
        float inputLean =
            -input.SteeringInput * 10f;

        // Speed amplifies instability
        float speedFactor =
            Mathf.Lerp(0.8f, 1.3f, speedController.Speed01);

        float targetLean =
            Mathf.Clamp(
                (velocityLean + inputLean) * speedFactor,
                -maxLeanAngle,
                maxLeanAngle
            );

        currentLean = Mathf.Lerp(
            currentLean,
            targetLean,
            leanSmooth * Time.deltaTime
        );

        ApplyLean(currentLean);
    }

    private void ApplyLean(float angle)
    {
        if (!bikeVisual) return;

        Vector3 rot = bikeVisual.localEulerAngles;
        rot.z = angle;
        bikeVisual.localEulerAngles = rot;
    }
}