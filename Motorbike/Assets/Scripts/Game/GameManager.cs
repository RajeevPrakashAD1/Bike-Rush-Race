using UnityEngine;
using UnityEngine.Audio;

namespace HazzardGameworks.ProjectInfrastructure.Game
{
    //This class is used for initializing and referencing game state
    //data like volume and display settings.

    public class GameManager : MonoBehaviour
    {
        //Static singleton reference to an instance of this class.
        public static GameManager Instance { get; private set; }

        public GameSettingsData Settings;

        //References for classes that control game-wide settings
        [SerializeField]
        private ScreenController screenController;
        [SerializeField]
        private AudioMixer audioMixer;

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
        private void Start()
        {
            //Load game settings when the game starts.
            Settings = SaveDataController.LoadGameSettings();

            //Set the AudioMixer values from the loaded game settings when the game starts.
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(Settings.MasterVolume) * 20);
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(Settings.MusicVolume) * 20);

            EventBus.Instance.OnSettingsChanged.AddListener(SaveGameSettings);
            
            //If this is the only scene, then load the intro scene.  This check is so that
            //the game scene can be open with other scenes in the editor and not load the
            //intro scene unnecessarily.
            if (UnityEngine.SceneManagement.SceneManager.sceneCount == 1)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0) 
                    SceneController.ChangeScene(2);
            }
        }

        /// <summary>
        /// Saves the current game settings after the OnSettingsChanged event has been raised
        /// by the game-wide Event Bus.
        /// </summary>
        /// <param name="settings"></param>
        public void SaveGameSettings(GameSettingsData settings)
        {
            SaveDataController.SaveGameSettings(settings);
        }
    }
}
