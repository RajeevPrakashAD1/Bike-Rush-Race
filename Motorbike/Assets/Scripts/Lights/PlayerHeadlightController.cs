using UnityEngine;

[RequireComponent(typeof(Light))]
public class PlayerHeadlightController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Range")]
    [SerializeField] private float minRange = 12f;
    [SerializeField] private float maxRange = 45f;
    [SerializeField] private AnimationCurve rangeBySpeed;

    [Header("Intensity (Gameplay, not realistic)")]
    [SerializeField] private float minIntensity = 10f;
    [SerializeField] private float maxIntensity = 1150f;
    [SerializeField] private AnimationCurve intensityBySpeed;

    [Header("Spot Angles")]
    [Tooltip("Defines suspicion width (rarely changes)")]
    [SerializeField] private float outerSpotAngle = 38f;

    [Tooltip("Defines certainty cone at low speed")]
    [SerializeField] private float minInnerSpotAngle = 10f;

    [Tooltip("Defines certainty cone at high speed")]
    [SerializeField] private float maxInnerSpotAngle = 18f;

    [SerializeField] private AnimationCurve innerAngleBySpeed;

    private Light headlight;

    private void Awake()
    {
        headlight = GetComponent<Light>();
        headlight.type = LightType.Spot;
        headlight.shadows = LightShadows.None;

        // Outer angle is mostly fixed
        headlight.spotAngle = outerSpotAngle;
    }

    private void Update()
    {
        float speed01 = speedController.Speed01;

        // -------- RANGE --------
        float rangeT = rangeBySpeed.Evaluate(speed01);
        headlight.range = Mathf.Lerp(minRange, maxRange, rangeT);

        // -------- INTENSITY --------
        float intensityT = intensityBySpeed.Evaluate(speed01);
        headlight.intensity = Mathf.Lerp(minIntensity, maxIntensity, intensityT);

        // -------- INNER SPOT ANGLE (CERTAINTY) --------
        float innerT = innerAngleBySpeed.Evaluate(speed01);
        headlight.innerSpotAngle = Mathf.Lerp(
            minInnerSpotAngle,
            maxInnerSpotAngle,
            innerT
        );
    }
}
