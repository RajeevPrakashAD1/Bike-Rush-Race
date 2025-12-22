using UnityEngine;
using UnityEngine.Events;

namespace HazzardGameworks.ProjectInfrastructure.Level
{
    /// <summary>
    /// Event bus for level-wide events.
    /// </summary>
    public class EventBus : MonoBehaviour
    {
        //Singleton reference.
        public static EventBus Instance { get; private set; }

        //Level-wide events.
        public UnityEvent OnBegin;
        public UnityEvent OnPause;
        public UnityEvent OnResume;
        public UnityEvent OnGameover;

        private void Awake()
        {
            //Singleton initialization.
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
