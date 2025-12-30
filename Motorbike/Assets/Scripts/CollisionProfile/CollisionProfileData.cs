using UnityEngine;

public enum CollisionType
{
    Soft,
    Hard,
    Fatal
}

[CreateAssetMenu(
    fileName = "CollisionProfile",
    menuName = "Game/Collision Profile"
)]
public class CollisionProfileData : ScriptableObject
{
    public CollisionType collisionType;

    [Range(0f, 1f)]
    public float impactStrength = 0.3f;

    public float lateralPush = 6f;
    public bool triggerWobble = true;
}