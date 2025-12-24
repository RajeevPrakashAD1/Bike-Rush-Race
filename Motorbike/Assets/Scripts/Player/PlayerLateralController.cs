using UnityEngine;

public class PlayerLateralController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameplayInputHandler input;
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Steering")]
    [SerializeField] private float baseInputForce = 30f;
    [SerializeField] private float minSteeringMultiplier = 0.25f; // at max speed

    [Header("Damping / Weight")]
    [SerializeField] private float dampingLowSpeed = 8f;
    [SerializeField] private float dampingHighSpeed = 22f;

    [Header("Lateral Velocity Limits")]
    [SerializeField] private float maxLatSpeedLow = 10f;
    [SerializeField] private float maxLatSpeedHigh = 3.5f;

    [Header("Road Boundaries")]
    [Tooltip("Half width of safe road area")]
    [SerializeField] private float safeHalfWidth = 6.5f;

    [SerializeField] private float wallResistanceLowSpeed = 20f;
    [SerializeField] private float wallResistanceHighSpeed = 70f;

    private float currentX;
    private float lateralVelocity;

    public float LateralVelocity => lateralVelocity;

    private void Start()
    {
        currentX = transform.position.x;
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        float speed01 = speedController.Speed01;

        float acceleration = 0f;

        // ==================================================
        // 1️⃣ SPEED-BASED STEERING AUTHORITY
        // ==================================================
        float steeringMultiplier = Mathf.Lerp(
            1f,
            minSteeringMultiplier,
            speed01
        );

        acceleration += input.SteeringInput * baseInputForce * steeringMultiplier;

        // ==================================================
        // 2️⃣ WALL RESISTANCE (ONLY WHEN HIT)
        // ==================================================
        float absX = Mathf.Abs(currentX);
        if (absX > safeHalfWidth)
        {
            float resistance = Mathf.Lerp(
                wallResistanceLowSpeed,
                wallResistanceHighSpeed,
                speed01
            );

            float pushDir = -Mathf.Sign(currentX);
            acceleration += pushDir * resistance;

            OnWallScrape(absX - safeHalfWidth);
        }

        // ==================================================
        // 3️⃣ INTEGRATE VELOCITY
        // ==================================================
        lateralVelocity += acceleration * dt;

        // ==================================================
        // 4️⃣ SPEED-BASED LATERAL SPEED LIMIT
        // ==================================================
        float maxLatSpeed = Mathf.Lerp(
            maxLatSpeedLow,
            maxLatSpeedHigh,
            speed01
        );

        lateralVelocity = Mathf.Clamp(
            lateralVelocity,
            -maxLatSpeed,
            maxLatSpeed
        );

        // ==================================================
        // 5️⃣ SPEED-BASED DAMPING (WEIGHT)
        // ==================================================
        float damping = Mathf.Lerp(
            dampingLowSpeed,
            dampingHighSpeed,
            speed01
        );

        lateralVelocity = Mathf.Lerp(
            lateralVelocity,
            0f,
            damping * dt
        );

        // ==================================================
        // 6️⃣ INTEGRATE POSITION
        // ==================================================
        currentX += lateralVelocity * dt;
        ApplyPosition();
    }

    private void ApplyPosition()
    {
        Vector3 pos = transform.position;
        pos.x = currentX;
        transform.position = pos;
    }

    private void OnWallScrape(float penetrationDepth)
    {
        // Later hook:
        // - Camera shake
        // - Sparks
        // - Speed loss
        // - Damage

        Debug.Log($"Wall scrape depth: {penetrationDepth:F2}");
    }
}
