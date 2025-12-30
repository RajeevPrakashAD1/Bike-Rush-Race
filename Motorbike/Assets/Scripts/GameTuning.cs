using UnityEngine;

[CreateAssetMenu(menuName = "NightRiders/Game Tuning")]
public class GameTuning : ScriptableObject
{
    // =========================
    // ROAD GENERATION
    // =========================
    [Header("Road Generation")]
    [Tooltip("Length of one road segment in Z")]
    public float roadSegmentLength = 107.3f;

    [Tooltip("How many segments stay ahead of the player")]
    public int segmentsAhead = 8;

    [Tooltip("How many segments stay behind the player")]
    public int segmentsBehind = 2;

    // =========================
    // LANES
    // =========================
    [Header("Lane Setup")]
    [Tooltip("Distance between lane centers")]
    public float laneWidth = 6.5f;

    [Header("Lane Change Speed")]
    [Tooltip("Lane change speed at low game speed")]
    public float laneChangeSpeedLow = 6f;

    [Tooltip("Lane change speed at high game speed")]
    public float laneChangeSpeedHigh = 2f;

    [Tooltip("Speed where controls feel fully heavy")]
    public float maxGameSpeed = 60f;

    // =========================
    // SPEED (FOR LATER USE)
    // =========================
    [Header("Speed")]
    public float cruiseSpeed = 20f;
    public float maxSpeed = 60f;
    [Header("Throttle Speed")]
    public float throttleAcceleration = 20f;

    [Tooltip("How fast speed falls back to cruise when throttle released")]
    public float speedReturnRate = 30f;

    //obstacle
    [Header("Obstacles")]
    [Range(0f, 1f)]
    public float obstacleSpawnChance = 0.6f;

}