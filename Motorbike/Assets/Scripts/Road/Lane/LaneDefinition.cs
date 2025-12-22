using UnityEngine;

public class LaneDefinition : MonoBehaviour
{
    [Header("Lane Setup")]
    public float laneWidth = 6.5f;   // distance between lane centers

    public float LeftLaneX  => -laneWidth * 0.5f;
    public float RightLaneX =>  laneWidth * 0.5f;

    public float GetLaneX(int laneIndex)
    {
        // laneIndex: 0 = left, 1 = right
        return laneIndex == 0 ? LeftLaneX : RightLaneX;
    }
    public float GetNearestLaneX(float x)
    {
        float left = GetLaneX(0);
        float right = GetLaneX(1);

        return Mathf.Abs(x - left) < Mathf.Abs(x - right)
            ? left
            : right;
    }
}