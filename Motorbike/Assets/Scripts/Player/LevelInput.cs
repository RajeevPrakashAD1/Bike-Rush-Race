using UnityEngine;
using HazzardGameworks.ProjectInfrastructure;

namespace HazzardGameworks.ProjectInfrastructure.Player
{
    /// <summary>
    /// Handles messages from a PlayerInput component during a gameplay level.
    /// </summary>
    public class LevelInput : MonoBehaviour
    {
        /// <summary>
        /// Handles pause input.
        /// </summary>
        public void OnPauseLevel()
        {
            Level.EventBus.Instance.OnPause?.Invoke();
        }
        /// <summary>
        /// Handles back input.
        /// </summary>
        public void OnBack()
        {
            
        }
    }
}
