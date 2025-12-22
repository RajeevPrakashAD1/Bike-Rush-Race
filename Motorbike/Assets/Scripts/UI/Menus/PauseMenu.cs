using UnityEngine;
using HazzardGameworks.ProjectInfrastructure;
using HazzardGameworks.ProjectInfrastructure.Game;

namespace HazzardGameworks.ProjectInfrastructure.UI
{
    /// <summary>
    /// This class contains functions for the pause menu GUI.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        // Reference to the pauseMenu panel for opening and closing.
        [SerializeField]
        private GameObject pauseMenu;

        private void Start()
        {
            if (Level.EventBus.Instance != null)
            {
                // Adds listener to the level Event Bus event.
                Level.EventBus.Instance.OnPause.AddListener(OpenPauseMenu);
            }
        }
        private void OnDisable()
        {
            Level.EventBus.Instance.OnPause.RemoveListener(OpenPauseMenu);
        }
        /// <summary>
        /// Opens the pause menu.
        /// </summary>
        public void OpenPauseMenu()
        {
            pauseMenu.SetActive(true);
        }
        /// <summary>
        /// Closes the pause menu and raises the OnResume event in the 
        /// level-wide Event Bus.
        /// </summary>
        public void ClosePauseMenu()
        {
            pauseMenu.SetActive(false);
            Level.EventBus.Instance.OnResume?.Invoke();
        }
        /// <summary>
        /// Returns to the main menu.  This should be refactored to raise
        /// an event such as OnLevelEnded.
        /// </summary>
        public void ExitLevel()
        {
            SceneController.ChangeScene(3);
        }
    }
}
