using UnityEngine;

public class ModuleSpawnTrigger : MonoBehaviour
{
    public RoadModule Module;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Module.SpawnNextModule(Module.Modules[Random.Range(0, Module.Modules.Count)]);

            if(ModuleSpawnManager.Instance != null)
            {
                RoadModule nextModule = Module.Modules[Random.Range(0, Module.Modules.Count)];
                Debug.Log("Next module is: " + nextModule.gameObject.name);
                if(nextModule != null)
                {
                    ModuleSpawnManager.Instance.AddModuleToQueue(nextModule);
                }
            }
            ModuleSpawnManager.Instance.UpdateSpawnPoint(Module.SpawnPoint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(Module.gameObject, 10f);
        }
    }
}
