using UnityEngine;
using HazzardGameworks.ProjectInfrastructure.Game;
using UnityEngine.UI;

namespace HazzardGameworks.ProjectInfrastructure.UI
{
    /// <summary>
    /// This class contains functions used by the video settings menu.
    /// </summary>
    public class VideoSettings : MonoBehaviour
    {
        // A reference to the toggle uGUI component.
        [SerializeField]
        private Toggle fullscreenToggle;

        private void OnEnable()
        {
            // Sets the toggle GUI component to match the current screen mode.
            if(Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            {
                fullscreenToggle.isOn = true;
            }
            else
            {
                fullscreenToggle.isOn = false;
            }
        }
        /// <summary>
        /// Raises the toggle screen mode event in the game-wide Event Bus,
        /// passing the current value of the screen mode GUI toggle component.
        /// </summary>
        public void ScreenModeToggle()
        {
            EventBus.Instance.OnFullscreenToggle?.Invoke(fullscreenToggle.isOn);
        }
    }
}
