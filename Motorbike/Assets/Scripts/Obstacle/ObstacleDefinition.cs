using UnityEngine;

public class ObstacleDefinition : MonoBehaviour
{
    [Header("Placement")]
    public float width = 1.5f;
    public float localZOffset = 0f;

    [Tooltip("Can this obstacle occupy the center of the road?")]
    public bool allowCenter = true;
}
