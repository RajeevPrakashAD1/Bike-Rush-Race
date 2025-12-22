using UnityEngine;
using UnityEngine.Events;

namespace HazzardGameworks.ProjectInfrastructure.Game
{
    //This class contains game-wide events like changes to volume or
    //the fullscreen mode.  Other classes can subscribe to these events
    //so that they react accordingly.

    public class EventBus : MonoBehaviour
    {
        //Static singleton reference to an instance of this class.
        public static EventBus Instance { get; private set; }

        //An event that can be invoked when the fullscreen mode is changed
        //from fullscreen to windowed or vice versa.
        public UnityEvent<bool> OnFullscreenToggle;

        // Should be raised when settings have been changed.
        public UnityEvent<GameSettingsData> OnSettingsChanged;

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
