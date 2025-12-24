using UnityEngine;

public class SpeedFOV : MonoBehaviour
{
    [SerializeField] private PlayerSpeedController speedController;
    [SerializeField] private Camera cam;

    [SerializeField] private float minFOV = 74f;
    [SerializeField] private float maxFOV = 100f;
    [SerializeField] private float smooth = 5f;

    private void Update()
    {
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, speedController.Speed01);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * smooth);
    }
}