using HazzardGameworks.ProjectInfrastructure.Game;
using UnityEngine;

namespace HazzardGameworks.ProjectInfrastructure.UI
{
    /// <summary>
    /// This class contains functions for the options menu.
    /// </summary>
    public class MainOptionsMenu : MonoBehaviour
    {
        /// <summary>
        /// Saves the current game settings.
        /// </summary>
        public void SaveGameSettings()
        {
            EventBus.Instance.OnSettingsChanged.Invoke(GameManager.Instance.Settings);
        }
    }
}
