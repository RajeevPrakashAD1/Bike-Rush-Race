using HazzardGameworks.ProjectInfrastructure.Game;
using UnityEngine;

namespace HazzardGameworks.ProjectInfrastructure.UI
{
    /// <summary>
    /// This class contains functions for the main menu GUI.
    /// This should probably be refactored to raise events on
    /// the game-wide Event Bus.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        public void PlayLevel(int level)
        {
            SceneController.ChangeScene(level);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
