using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    [SerializeField] private PlayerSpeedController speedController;

    [Header("Shake Amount")]
    [SerializeField] private float minAmplitude = 0.01f;
    [SerializeField] private float maxAmplitude = 0.06f;

    [Header("Shake Frequency")]
    [SerializeField] private float minFrequency = 6f;
    [SerializeField] private float maxFrequency = 18f;

    private Vector3 startLocalPos;
    private float noiseSeed;

    private void Awake()
    {
        startLocalPos = transform.localPosition;
        noiseSeed = Random.value * 100f;
    }

    private void LateUpdate()
    {
        float speed01 = speedController.Speed01;

        float amp = Mathf.Lerp(minAmplitude, maxAmplitude, speed01);
        float freq = Mathf.Lerp(minFrequency, maxFrequency, speed01);

        float x = (Mathf.PerlinNoise(Time.time * freq, noiseSeed) - 0.5f) * amp;
        float y = (Mathf.PerlinNoise(noiseSeed, Time.time * freq) - 0.5f) * amp;

        transform.localPosition = startLocalPos + new Vector3(x, y, 0f);
    }
}