using UnityEngine;

public class PlayerLateralController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameplayInputHandler input;
    [SerializeField] private PlayerSpeedController speedController;
    [SerializeField] private LaneDefinition lanes;

    [Header("Input Control")]
    [SerializeField] private float inputForce = 30f;
    [SerializeField] private float damping = 10f;

    [Header("Lane Stability")]
    [Tooltip("How strongly bike recenters when NO input is given")]
    [SerializeField] private float lanePullLowSpeed = 15f;
    [SerializeField] private float lanePullHighSpeed = 60f;

    [Tooltip("Max sideways distance allowed")]
    [SerializeField] private float maxLaneOffset = 5f;

    [Tooltip("Input threshold to consider player actively steering")]
    [SerializeField] private float steerDeadZone = 0.1f;

    private float lateralVelocity;
    private float currentX;

    public float LateralVelocity => lateralVelocity;

    private void Start()
    {
        currentX = transform.position.x;
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        float speed01 = speedController.Speed01;
        float steer = input.SteeringInput;

        float acceleration = 0f;

        // ============================
        // 1️⃣ PLAYER CONTROL (PRIMARY)
        // ============================
        if (Mathf.Abs(steer) > steerDeadZone)
        {
            // Player is actively steering → FULL control
            acceleration += steer * inputForce;
        }
        else
        {
            // ============================
            // 2️⃣ LANE STABILITY (SECONDARY)
            // ============================
            float targetLaneX = lanes.GetNearestLaneX(currentX);

            float lanePullStrength = Mathf.Lerp(
                lanePullLowSpeed,
                lanePullHighSpeed,
                speed01
            );

            acceleration += (targetLaneX - currentX) * lanePullStrength;
        }

        // ============================
        // 3️⃣ INTEGRATE VELOCITY
        // ============================
        lateralVelocity += acceleration * dt;

        // ============================
        // 4️⃣ DAMPING (BIKE WEIGHT)
        // ============================
        lateralVelocity = Mathf.Lerp(
            lateralVelocity,
            0f,
            damping * dt
        );

        // ============================
        // 5️⃣ INTEGRATE POSITION
        // ============================
        currentX += lateralVelocity * dt;

        // ============================
        // 6️⃣ SAFETY CLAMP
        // ============================
        currentX = Mathf.Clamp(
            currentX,
            -maxLaneOffset,
            maxLaneOffset
        );

        ApplyPosition();
    }

    private void ApplyPosition()
    {
        Vector3 pos = transform.position;
        pos.x = currentX;
        transform.position = pos;
    }
}
