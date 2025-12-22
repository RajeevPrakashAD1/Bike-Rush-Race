using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private RoadSegment[] roadPrefabs;
    [SerializeField] private GameTuning tuning;
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private readonly Queue<RoadSegment> activeSegments = new Queue<RoadSegment>();
    private float nextSpawnZ;

    private void Start()
    {
        if (!tuning)
        {
            Debug.LogError("GameTuning not assigned!");
            return;
        }

        int initialCount = tuning.segmentsAhead + tuning.segmentsBehind;

        for (int i = 0; i < initialCount; i++)
        {
            SpawnSegment();
        }
    }

    private void Update()
    {
        float playerZ = player.position.z;

        while (activeSegments.Count > 0 &&
               activeSegments.Peek().transform.position.z + tuning.roadSegmentLength <
               playerZ - (tuning.segmentsBehind * tuning.roadSegmentLength))
        {
            RecycleSegment();
        }
    }

    private void SpawnSegment()
    {
        RoadSegment prefab = roadPrefabs[Random.Range(0, roadPrefabs.Length)];

        RoadSegment segment = Instantiate(
            prefab,
            new Vector3(0f, 0f, nextSpawnZ),
            Quaternion.identity,
            transform
        );

        activeSegments.Enqueue(segment);
        nextSpawnZ += tuning.roadSegmentLength;
        obstacleSpawner.OnRoadSegmentSpawned(segment.transform.position.z);

    }

    private void RecycleSegment()
    {
        RoadSegment segment = activeSegments.Dequeue();
        segment.transform.position = new Vector3(0f, 0f, nextSpawnZ);
        activeSegments.Enqueue(segment);

        nextSpawnZ += tuning.roadSegmentLength;
        obstacleSpawner.OnRoadSegmentSpawned(segment.transform.position.z);

    }
}