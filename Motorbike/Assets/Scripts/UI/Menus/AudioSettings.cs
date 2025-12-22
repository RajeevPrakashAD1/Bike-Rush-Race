using UnityEngine;
using UnityEngine.Audio;

namespace HazzardGameworks.ProjectInfrastructure.Game
{
    /// <summary>
    /// This class contains functions for the audio settings GUI.  This
    /// should be refactored so that the changes to the AudioMixer
    /// are made in an AudioController class, so that this class only
    /// affects the GUI and not the AudioMixer directly.  Functions
    /// in the AudioController should be handled via the Game Event Bus.
    /// </summary>
    public class AudioSettings : MonoBehaviour
    {
        // Reference to the AudioMixer asset.  This should be moved to an
        // AudioController class.
        [SerializeField]
        private AudioMixer audioMixer;

        /// <summary>
        /// This class should be refactored so that it invokes and audio
        /// change event in the EventBus, and passes the slider value
        /// to the event.
        /// </summary>
        /// <param name="value"></param>
        public void ChangeMasterVolume(float value)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
            GameManager.Instance.Settings.MasterVolume = value;
        }
        /// <summary>
        /// This class should be refactored so that it invokes and audio
        /// change event in the EventBus, and passes the slider value
        /// to the event.
        /// </summary>
        /// <param name="value"></param>
        public void ChangeMusicVolume(float value)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
            GameManager.Instance.Settings.MusicVolume = value;
        }


    }
}
