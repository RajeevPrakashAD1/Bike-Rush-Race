
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HazzardGameworks.ProjectInfrastructure.Game
{
    public class LoadingScreen : MonoBehaviour
    {
        /// <summary>
        /// This class handles transitions between scenes.  It will never 
        /// unload the game scene, which contains references to classes
        /// and functions that may need to be accessed at any point in the 
        /// game.
        /// </summary>
        private void Start()
        {
            SceneController.CurrentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            StartCoroutine(ChangingScenes(SceneController.NextScene, SceneController.CurrentScene));
        }

        /// <summary>
        /// Controls the loading of the next scene and unloading of the previous scene.
        /// </summary>
        /// <param name="nextScene"></param>
        /// <param name="prevScene"></param>
        /// <returns></returns>
        private IEnumerator ChangingScenes(int nextScene, int prevScene)
        {
            Debug.Log("Changing scenes.");
            yield return LoadingNextScene(nextScene);
            yield return UnloadingPreviousScene(prevScene);

            SceneManager.UnloadSceneAsync(1);
        }
        /// <summary>
        /// Loads the next scene.
        /// </summary>
        /// <param name="nextScene"></param>
        /// <returns></returns>
        private IEnumerator LoadingNextScene(int nextScene)
        {
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            loadAsync.allowSceneActivation = false;

            while (loadAsync.progress < 0.9f)
            {
                Debug.Log("Loading progress: " + loadAsync.progress + "%.");
                yield return null;
            }

            loadAsync.allowSceneActivation = true;

            while (!loadAsync.isDone)
            {
                yield return null;
            }
            yield return null;

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextScene));
        }
        /// <summary>
        /// Unloads the previous scene.
        /// </summary>
        /// <param name="prevScene"></param>
        /// <returns></returns>
        private IEnumerator UnloadingPreviousScene(int prevScene)
        {
            Debug.Log("Unloading previous scene.");
            if (prevScene != 0)
            {
                AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(prevScene);

                while (!unloadAsync.isDone)
                {
                    Debug.Log("Unloading progress: " + unloadAsync.progress + "%.");
                    yield return null;
                }
            }
        }
    }
}
