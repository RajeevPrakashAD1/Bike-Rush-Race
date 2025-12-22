using UnityEngine;

namespace HazzardGameworks.ProjectInfrastructure.Game
{
    /// <summary>
    /// This class contains methods to control the screen.
    /// </summary>
    public class ScreenController : MonoBehaviour
    {
        private void Start()
        {
            //Subscribes to the fullscreen change event in the game-wide event bus.
            EventBus.Instance.OnFullscreenToggle.AddListener(ToggleFullscreen);
        }

        /// <summary>
        /// Toggles the fullscreen mode between fullscreen and windowed.
        /// </summary>
        /// <param name="fullscreen"></param>
        public void ToggleFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                //Makes sure the resolution is set to 1920 x 1080 when set to fullscreen.
                //This is not ideal.
                Screen.SetResolution(1920, 1080, true);
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }
        }
    }
}
