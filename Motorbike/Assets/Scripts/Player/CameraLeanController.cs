using UnityEngine;

public class CameraLeanController : MonoBehaviour
{
    [SerializeField] private PlayerLaneController laneController;
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Camera Lean")]
    [SerializeField] private float maxRoll = 8f;
    [SerializeField] private float maxYaw = 4f;
    [SerializeField] private float smooth = 6f;

    private float currentRoll;
    private float currentYaw;

    private void LateUpdate()
    {
        float targetRoll = 0f;
        float targetYaw = 0f;

        if (laneController.IsSwitching)
        {
            int dir = laneController.TargetLane > laneController.CurrentLane ? -1 : 1;

            float speedFactor = Mathf.Lerp(0.6f, 1.1f, speedController.Speed01);

            targetRoll = dir * maxRoll * speedFactor;
            targetYaw  = dir * maxYaw  * speedFactor;
        }

        currentRoll = Mathf.Lerp(currentRoll, targetRoll, smooth * Time.deltaTime);
        currentYaw  = Mathf.Lerp(currentYaw,  targetYaw,  smooth * Time.deltaTime);

        ApplyCameraLean(currentRoll, currentYaw);
    }

    private void ApplyCameraLean(float roll, float yaw)
    {
        transform.localRotation = Quaternion.Euler(0f, yaw, roll);
    }
}