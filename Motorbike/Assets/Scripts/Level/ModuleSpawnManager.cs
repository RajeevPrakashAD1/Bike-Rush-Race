using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSpawnManager : MonoBehaviour
{
    public static ModuleSpawnManager Instance;

    private List<RoadModule> moduleQueue;
    [SerializeField]
    private List<RoadModule> objectiveModuleQueue;

    private Transform currentSpawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void UpdateSpawnPoint(Transform spawnPoint)
    {
        currentSpawnPoint = spawnPoint;
    }

    public void AddModuleToQueue(RoadModule module)
    {
        moduleQueue.Add(module);
    }

    public void SpawnModule()
    {
        if (moduleQueue.Count > 0)
        {
            RoadModule module = GameObject.Instantiate(moduleQueue[0], currentSpawnPoint.position, currentSpawnPoint.rotation);
        }
        else Debug.Log("Module queue is empty.");
    }
}
