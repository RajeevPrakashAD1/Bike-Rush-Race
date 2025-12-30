using UnityEngine;

public class PlayerBendDriftController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerLateralController lateral;
    [SerializeField] private PlayerSpeedController speed;
    [SerializeField] private CameraBendRollController cameraBend;

    [Header("Drift Tuning")]
    [Tooltip("Base sideways force caused by road bend")]
    [SerializeField] private float baseDriftForce = 4f;

    [Tooltip("How much stronger drift becomes at high speed")]
    [SerializeField] private float maxSpeedMultiplier = 1.6f;

    [Tooltip("Ignore very small bends")]
    [SerializeField] private float deadZone = 0.05f;

    private void Update()
    {
        if (!cameraBend || !lateral || !speed)
            return;

        float bend = cameraBend.CurrentBend;

        if (Mathf.Abs(bend) < deadZone)
            return;

        float speed01 = speed.Speed01;

        // Simulated centrifugal force
        float driftForce =
            bend *
            baseDriftForce *
            Mathf.Lerp(1f, maxSpeedMultiplier, speed01) *
            Time.deltaTime;

        lateral.AddLateralImpulse(driftForce);
    }
}