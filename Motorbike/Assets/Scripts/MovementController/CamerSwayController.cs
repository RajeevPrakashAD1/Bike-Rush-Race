using UnityEngine;

public class CameraSwayController : MonoBehaviour
{
    [SerializeField] private float maxLeanAngle = 4f;
    [SerializeField] private float leanSmooth = 6f;

    private float targetLean;

    public void SetLean(float turnInput)
    {
        targetLean = -turnInput * maxLeanAngle;
    }

    private void LateUpdate()
    {
        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetLean);
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRot,
            Time.deltaTime * leanSmooth
        );
    }
}