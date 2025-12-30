using UnityEngine;

public class SpeedHeadlightController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerSpeedController speed;
    [SerializeField] private Light headlight;

    [Header("Range")]
    [SerializeField] private float rangeSlow = 18f;
    [SerializeField] private float rangeFast = 40f;

    [Header("Intensity")]
    [SerializeField] private float intensitySlow = 4f;
    [SerializeField] private float intensityFast = 9f;

    [Header("Spot Angle")]
    [SerializeField] private float angleSlow = 40f;
    [SerializeField] private float angleFast = 28f;

    private void Update()
    {
        float speed01 = speed.Speed01;

        headlight.range = Mathf.Lerp(
            rangeSlow,
            rangeFast,
            speed01
        );

        headlight.intensity = Mathf.Lerp(
            intensitySlow,
            intensityFast,
            speed01
        );

        headlight.spotAngle = Mathf.Lerp(
            angleSlow,
            angleFast,
            speed01
        );
    }
}