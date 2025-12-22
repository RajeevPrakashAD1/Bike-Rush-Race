using System.Collections.Generic;
using UnityEngine;

public class RoadModule : MonoBehaviour
{
    public List<RoadModule> Modules;
    public Transform SpawnPoint;
    public GameObject PreviousModule;

    public void SpawnNextModule(RoadModule module)
    {
        RoadModule nextModule = GameObject.Instantiate(module, SpawnPoint.position, SpawnPoint.rotation);
        nextModule.PreviousModule = gameObject;
        if(PreviousModule != null )
        {
            Destroy(PreviousModule);
        }
    }
}
