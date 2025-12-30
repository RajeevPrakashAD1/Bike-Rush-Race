using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerWobbleController wobble;
    [SerializeField] private PlayerLateralController lateral;
    [SerializeField] private PlayerSpeedController speedController;
    [SerializeField] private PlayerHealth health;

    [Header("Speed Scaling")]
    [Tooltip("Extra punishment at high speed")]
    [SerializeField] private AnimationCurve impactBySpeed =
        AnimationCurve.EaseInOut(0, 0.3f, 1, 1.2f);

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger collisiion");
        
        CollisionProfile profile = other.GetComponent<CollisionProfile>();
        if (profile == null)
        {
            Debug.Log("No collision profile"+other.transform.name);
            return;
        }
        health.TakeDamage(profile.ImpactStrength * 20f);


        float speed01 = speedController.Speed01;
        float speedMultiplier = impactBySpeed.Evaluate(speed01);

        // =========================
        // FATAL COLLISION
        // =========================
        if (profile.CollisionType == CollisionType.Fatal)
        {
            //lateral.ForceCrash();
            return;
        }

        // =========================
        // WOBBLE
        // =========================
        if (profile.triggerWobble)
        {
            float wobbleImpulse =
                profile.impactStrength * speedMultiplier;

            //wobble.TriggerWobble(wobbleImpulse);
        }

        // =========================
        // LATERAL PUSH
        // =========================
        Vector3 hitDir = transform.position - other.transform.position;
        float pushDir = Mathf.Sign(hitDir.x);

        float pushForce =
            profile.lateralPush * speedMultiplier;

       lateral.AddLateralImpulse(pushDir * pushForce);
    }
}