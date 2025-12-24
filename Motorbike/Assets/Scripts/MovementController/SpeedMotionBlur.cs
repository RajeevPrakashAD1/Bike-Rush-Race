using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpeedMotionBlur : MonoBehaviour
{
    [SerializeField] private PlayerSpeedController speedController;
    [SerializeField] private Volume volume;

    private MotionBlur blur;

    private void Awake()
    {
        volume.profile.TryGet(out blur);
    }

    private void Update()
    {
        blur.intensity.value = Mathf.Lerp(
            0.1f,
            0.55f,
            speedController.Speed01
        );
    }
}