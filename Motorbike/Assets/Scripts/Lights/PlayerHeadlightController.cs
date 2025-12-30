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
    [SerializeField] private float minIntensity = 800f;
    [SerializeField] private float maxIntensity = 1150f;
    [SerializeField] private AnimationCurve intensityBySpeed;



    [Tooltip("Defines certainty cone at low speed")]
    [SerializeField] private float minInnerSpotAngle = 43;

    [Tooltip("Defines certainty cone at high speed")]
    [SerializeField] private float maxInnerSpotAngle = 101f;

    [Tooltip("Defines certainty cone at low speed")]
    [SerializeField] private float minOuterSpotAngle = 54;

    [Tooltip("Defines certainty cone at high speed")]
    [SerializeField] private float maxOuterSpotAngle = 102;
    
    [SerializeField] private AnimationCurve innerAngleBySpeed;
    [SerializeField] private AnimationCurve outerAngleBySpeed;

    private Light headlight;

    private void Awake()
    {
        headlight = GetComponent<Light>();
        headlight.type = LightType.Spot;
        headlight.shadows = LightShadows.None;

        // Outer angle is mostly fixed
        //headlight.spotAngle = outerSpotAngle;
    }

    private void Update()
    {
        float speed01 = speedController.Speed01;

        // -------- RANGE --------
        float rangeT = rangeBySpeed.Evaluate(speed01);
        rangeT = Mathf.Pow(rangeT, 0.6f);
        headlight.range = Mathf.Lerp(minRange, maxRange, rangeT);

        // -------- INTENSITY --------
        float intensityT = intensityBySpeed.Evaluate(speed01);
        intensityT = Mathf.Pow(intensityT, 0.6f);
        headlight.intensity = Mathf.Lerp(minIntensity, maxIntensity, intensityT);

        // Angles (safe)
        float inner = Mathf.Lerp(minInnerSpotAngle, maxInnerSpotAngle, innerAngleBySpeed.Evaluate(speed01));
        float outer = Mathf.Lerp(minOuterSpotAngle, maxOuterSpotAngle, outerAngleBySpeed.Evaluate(speed01));

        headlight.spotAngle = outer;
        headlight.innerSpotAngle = Mathf.Min(inner, outer);
    }
}
