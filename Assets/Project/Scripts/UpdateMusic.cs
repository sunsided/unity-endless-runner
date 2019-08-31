using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class UpdateMusic : MonoBehaviour
    {
        public float defaultVolume = 0.25f;
        private readonly List<AudioSource> _music = new List<AudioSource>();
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            var audioSources = GameObject.FindWithTag("GameData").GetComponentsInChildren<AudioSource>();

            // Note that we're picking the FIRST audio source here, as that's the music.
            Debug.Assert(audioSources.Length > 0, "allAS.Length > 0");
            _music.Add(audioSources[0]);


            if (PlayerPrefs.HasKey(PlayerPrefKeys.MusicVolume))
            {
                var storedValue = PlayerPrefs.GetFloat(PlayerPrefKeys.MusicVolume);
                _slider.value = storedValue;
                UpdateMusicVolume(storedValue);
            }
            else
            {
                _slider.value = defaultVolume;
                UpdateMusicVolume(defaultVolume);
            }
        }

        public void UpdateMusicVolume() => UpdateMusicVolume(_slider.value);

        private void UpdateMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat(PlayerPrefKeys.MusicVolume, volume);
            foreach (var source in _music)
            {
                source.volume = volume;
            }
        }
    }
}
