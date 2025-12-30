using UnityEngine;

public class SpeedFogURPController : MonoBehaviour
{
    [SerializeField] private PlayerSpeedController speed;

    [Header("Fog Density")]
    [SerializeField] private float fogSlow = 0.04f;
    [SerializeField] private float fogFast = 0.008f;

    [Header("Fog Color")]
    [SerializeField] private Color fogColorSlow = new Color(0.15f, 0.15f, 0.15f);
    [SerializeField] private Color fogColorFast = new Color(0.05f, 0.05f, 0.05f);

    [SerializeField] private float smooth = 2f;

    private void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
    }

    private void Update()
    {
        float speed01 = speed.Speed01;

        RenderSettings.fogDensity = Mathf.Lerp(
            RenderSettings.fogDensity,
            Mathf.Lerp(fogSlow, fogFast, speed01),
            Time.deltaTime * smooth
        );

      
    }
}