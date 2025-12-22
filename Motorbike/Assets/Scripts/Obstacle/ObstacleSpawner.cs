using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameTuning tuning;
    [SerializeField] private Transform player;
    [SerializeField] private LaneDefinition lanes;

    [Header("Obstacle Setup")]
    [SerializeField] private ObstacleDefinition[] obstaclePrefabs;

    [Tooltip("How many segments ahead obstacles can appear")]
    [SerializeField] private int spawnAheadSegments = 6;

    private float nextSpawnZ;

    private readonly List<GameObject> activeObstacles = new List<GameObject>();

    private void Start()
    {
        nextSpawnZ = player.position.z;
    }

    public void OnRoadSegmentSpawned(float segmentStartZ)
    {
        // Random chance per segment
        if (Random.value > tuning.obstacleSpawnChance)
            return;

        SpawnObstacle(segmentStartZ);
    }

    private void SpawnObstacle(float segmentStartZ)
    {
        ObstacleDefinition prefab =
            obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        float laneX = lanes.GetLaneX(Random.Range(0,2));
        float z = segmentStartZ + prefab.localZOffset;

        Vector3 pos = new Vector3(laneX, 0f, z);

        GameObject obj = Instantiate(prefab.gameObject, pos, Quaternion.identity);
        activeObstacles.Add(obj);
    }
}