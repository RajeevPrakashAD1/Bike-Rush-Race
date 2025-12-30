using UnityEngine;

public class CollisionProfile : MonoBehaviour
{
    [Header("Profile")]
    public CollisionProfileData data;

    [Header("Overrides (Optional)")]
    public bool overrideImpact;
    public float impactStrength;

    public bool overridePush;
    public float lateralPush;

    public bool overrideWobble;
    public bool triggerWobble;

    public CollisionType CollisionType =>
        data.collisionType;

    public float ImpactStrength =>
        overrideImpact ? impactStrength : data.impactStrength;

    public float LateralPush =>
        overridePush ? lateralPush : data.lateralPush;

    public bool TriggerWobble =>
        overrideWobble ? triggerWobble : data.triggerWobble;
}