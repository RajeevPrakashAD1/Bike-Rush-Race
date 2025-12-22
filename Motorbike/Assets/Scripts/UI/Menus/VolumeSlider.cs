using HazzardGameworks.ProjectInfrastructure.Game;
using UnityEngine;
using UnityEngine.UI;

namespace HazzardGameworks.ProjectInfrastructure.UI
{
    /// <summary>
    /// This class contains functions used by the volume
    /// sliders in the audio settings menu.
    /// </summary>
    public class VolumeSlider : MonoBehaviour
    {
        // String reference used to indentify which type of volume
        // this slider is used to adjust.  Should match one of the
        // volume values in the game settings struct.
        [SerializeField]
        private string volume;
        // The slider uGUI component.
        [SerializeField]
        private Slider slider;

        private void OnEnable()
        {
            // Sets the volume slider values to match the current game settings
            // when the menu is enabled.
            switch(volume)
            {
                case "MasterVolume":
                    slider.value = GameManager.Instance.Settings.MasterVolume;
                    break;
                case "MusicVolume":
                    slider.value = GameManager.Instance.Settings.MusicVolume;
                    break;
            }
        }
    }
}
