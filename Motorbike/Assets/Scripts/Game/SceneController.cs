using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HazzardGameworks.ProjectInfrastructure.Game
{
    /// <summary>
    /// Static class that controls changing scenes.
    /// </summary>
    public static class SceneController
    {
        /// <summary>
        /// Build index references for the current and next scene.
        /// </summary>
        private static int currentScene;
        private static int nextScene;

        /// <summary>
        /// Public properties to access the current and next scene build
        /// index fields.
        /// </summary>
        public static int CurrentScene
        {
            get
            {
                return currentScene;
            }
            set
            {
                currentScene = value;
            }
        }
        public static int NextScene
        {
            get
            {
                return nextScene;
            }
            set
            {
                nextScene = value;
            }
        }

        /// <summary>
        /// Loads the loading screen scene to transition scenes.
        /// </summary>
        /// <param name="scene"></param>
        public static void ChangeScene(int scene)
        {
            nextScene = scene;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
