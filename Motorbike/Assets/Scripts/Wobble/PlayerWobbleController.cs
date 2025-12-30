using UnityEngine;

public class PlayerWobbleController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameplayInputHandler input;
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Thresholds")]
    [SerializeField] private float wobbleStartThreshold = 0.2f;
    [SerializeField] private float crashThreshold = 1.0f;

    [Header("Build / Decay")]
    [SerializeField] private float baseWobbleFromHit = 0.25f;
    [SerializeField] private float wobbleGrowthFromInput = 1.2f;
    [SerializeField] private float wobbleDecayRate = 0.6f;

    [Header("Speed Scaling")]
    [Tooltip("How wobble scales with speed")]
    [SerializeField] private AnimationCurve wobbleBySpeed =
        AnimationCurve.EaseInOut(0, 0.2f, 1, 1f);

    [Header("Motion")]
    [SerializeField] private float baseWobbleForce = 25f;
    [SerializeField] private float baseWobbleFrequency = 14f;
    private bool debugLockWobble = true;

    private float wobbleAmount;
    private float wobbleTimer;

    public float WobbleAmount => wobbleAmount;
    public bool IsWobbling => wobbleAmount > wobbleStartThreshold;

    // ==================================================
    // CALLED BY WALLS / OBSTACLES
    // ==================================================
    public void TriggerWobble(float impactStrength)
    {
        float speed01 = speedController.Speed01;

        float speedMultiplier = wobbleBySpeed.Evaluate(speed01);

        float impulse = baseWobbleFromHit * impactStrength * speedMultiplier;
        Debug.Log("increasing wobble amount");
        wobbleAmount = Mathf.Clamp01(wobbleAmount + impulse);
    }

    // ==================================================
    // CALLED EVERY FRAME BY MOVEMENT
    // ==================================================
    public float GetWobbleForce(float dt)
    {
        float steering = input.SteeringInput;

        // ----------------------------------
        // ENERGY RULE
        // ----------------------------------
        if (Mathf.Abs(steering) < 0.1f)
        {
            wobbleAmount -= wobbleDecayRate * dt;
        }
        else
        {
            wobbleAmount += Mathf.Abs(steering) * wobbleGrowthFromInput * dt;
        }

        wobbleAmount = Mathf.Clamp01(wobbleAmount);

        if (!IsWobbling)
        {
            wobbleTimer = 0f;
            return 0f;
        }

        wobbleTimer += dt;

        float speed01 = speedController.Speed01;

        float force = Mathf.Lerp(
            baseWobbleForce * 0.5f,
            baseWobbleForce,
            speed01
        );

        float frequency = Mathf.Lerp(
            baseWobbleFrequency * 0.8f,
            baseWobbleFrequency * 1.4f,
            speed01
        );

        return Mathf.Sin(wobbleTimer * frequency) * force * wobbleAmount;
    }

    public bool HasCrashed()
    {
        return wobbleAmount >= crashThreshold;
    }

    public void ResetWobble()
    {
        wobbleAmount = 0f;
        wobbleTimer = 0f;
    }
}
