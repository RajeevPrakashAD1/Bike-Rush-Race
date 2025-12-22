using UnityEngine;

public class ObstacleDefinition : MonoBehaviour
{
    [Tooltip("Which lane this obstacle occupies (0 = Left, 1 = Right)")]
    public int laneIndex;

    [Tooltip("Z offset inside the road segment")]
    public float localZOffset;
}