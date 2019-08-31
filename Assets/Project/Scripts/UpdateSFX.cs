using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class UpdateSFX : MonoBehaviour
    {
        public float defaultVolume = 0.5f;
        private readonly List<AudioSource> _sfx = new List<AudioSource>();
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            var audioSources = GameObject.FindWithTag("GameData").GetComponentsInChildren<AudioSource>();

            // Note that we're NOT picking the first audio player here, because that's the music.
            Debug.Assert(audioSources.Length > 1, "allAS.Length > 1");
            for (var i = 1; i < audioSources.Length; ++i)
            {
                _sfx.Add(audioSources[i]);
            }

            if (PlayerPrefs.HasKey(PlayerPrefKeys.SoundVolume))
            {
                var storedValue = PlayerPrefs.GetFloat(PlayerPrefKeys.SoundVolume);
                _slider.value = storedValue;
                UpdateSoundVolume(storedValue);
            }
            else
            {
                _slider.value = defaultVolume;
                UpdateSoundVolume(defaultVolume);
            }
        }

        public void UpdateSoundVolume() => UpdateSoundVolume(_slider.value);

        private void UpdateSoundVolume(float volume)
        {
            PlayerPrefs.SetFloat(PlayerPrefKeys.SoundVolume, volume);
            foreach (var source in _sfx)
            {
                source.volume = volume;
            }
        }
    }
}
