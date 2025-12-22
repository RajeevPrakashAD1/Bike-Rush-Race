using UnityEngine;

public class CameraLeanController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerLateralController lateralController;
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Camera Motion")]
    [SerializeField] private float maxRoll = 7f;
    [SerializeField] private float maxYaw = 4f;
    [SerializeField] private float smooth = 5f;

    private float roll;
    private float yaw;

    private void LateUpdate()
    {
        float lateralVel = lateralController.LateralVelocity;

        float speedFactor =
            Mathf.Lerp(0.7f, 1.2f, speedController.Speed01);

        float targetRoll =
            Mathf.Clamp(
                -lateralVel * 0.25f * speedFactor,
                -maxRoll,
                maxRoll
            );

        float targetYaw =
            Mathf.Clamp(
                -lateralVel * 0.15f * speedFactor,
                -maxYaw,
                maxYaw
            );

        roll = Mathf.Lerp(roll, targetRoll, smooth * Time.deltaTime);
        yaw  = Mathf.Lerp(yaw,  targetYaw,  smooth * Time.deltaTime);

        transform.localRotation =
            Quaternion.Euler(0f, yaw, roll);
    }
}