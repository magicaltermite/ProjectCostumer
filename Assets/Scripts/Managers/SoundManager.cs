using UnityEngine;
using UnityEngine.UI;

namespace Managers {
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

        // Our slider from Unity
        [SerializeField] private Slider volumeSlider;


        void Start()
        {
            // PlayerPrefs = Players settings, if there is no setting saved...
            if (!PlayerPrefs.HasKey("volumeMusic"))
            {
                // ...Set the volume to 100% (1f)
                PlayerPrefs.SetFloat("volumeMusic", 1);

                // And load it
                Load();
            }
            else
            {
                // If there is a player setting that has been saved, load it. 
                Load();
            }
        }

        public void changeVolume()
        {
            // Used in Unity editor - This is how we connect our audio to the slider, and after it has been changed, it gets saved
            AudioListener.volume = volumeSlider.value;
            Save();

        }

        public void Save()
        {
            // Saves the settings in the key "volumeMusic"
            PlayerPrefs.SetFloat("volumeMusic", volumeSlider.value);
        }

        public void Load()
        {
            // Loads the setting from the key "volumeMusic"
            volumeSlider.value = PlayerPrefs.GetFloat("volumeMusic");
        }
    }
}
