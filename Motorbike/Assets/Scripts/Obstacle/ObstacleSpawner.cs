using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameTuning tuning;
    [SerializeField] private Transform player;

    [Header("Road")]
    [SerializeField] private float roadHalfWidth = 6.5f;

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
        if (Random.value > tuning.obstacleSpawnChance)
            return;

        SpawnObstacle(segmentStartZ);
    }

    private void SpawnObstacle(float segmentStartZ)
    {
        ObstacleDefinition prefab =
            obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        float halfObstacleWidth = prefab.width * 0.5f;

        float minX = -roadHalfWidth + halfObstacleWidth;
        float maxX =  roadHalfWidth - halfObstacleWidth;

        float x;

        if (prefab.allowCenter)
        {
            // Full road allowed
            x = Random.Range(minX, maxX);
        }
        else
        {
            // Exclude center zone
            float centerExclusionHalf = 0.75f; // tweakable

            float leftMin = minX;
            float leftMax = -centerExclusionHalf - halfObstacleWidth;

            float rightMin = centerExclusionHalf + halfObstacleWidth;
            float rightMax = maxX;

            bool spawnLeft = Random.value < 0.5f;

            if (spawnLeft && leftMax > leftMin)
                x = Random.Range(leftMin, leftMax);
            else
                x = Random.Range(rightMin, rightMax);
        }

        float z = segmentStartZ + prefab.localZOffset;
        Vector3 pos = new Vector3(x, 0f, z);

        GameObject obj = Instantiate(prefab.gameObject, pos, Quaternion.identity);
        activeObstacles.Add(obj);
    }

}