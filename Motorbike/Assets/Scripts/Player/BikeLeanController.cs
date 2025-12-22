using UnityEngine;

public class BikeLeanController : MonoBehaviour
{
    [SerializeField] private PlayerLaneController laneController;
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Lean Settings")]
    [SerializeField] private Transform bikeVisual;
    [SerializeField] private float maxLeanAngle = 20f;
    [SerializeField] private float leanSmooth = 8f;

    private float currentLean;

    private void Update()
    {
        float targetLean = 0f;

        if (laneController.IsSwitching)
        {
            int direction = laneController.TargetLane > laneController.CurrentLane ? -1 : 1;

            float speedFactor = Mathf.Lerp(0.7f, 1.2f, speedController.Speed01);

            targetLean = direction * maxLeanAngle * speedFactor;
        }

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