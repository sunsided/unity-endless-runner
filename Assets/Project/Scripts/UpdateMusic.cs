using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class UpdateMusic : MonoBehaviour
    {
        private readonly List<AudioSource> _music = new List<AudioSource>();

        private void Start()
        {
            var allAS = GameObject.FindWithTag("GameData").GetComponentsInChildren<AudioSource>();
            Debug.Assert(allAS.Length > 0, "allAS.Length > 0");
            _music.Add(allAS[0]);
        }

        public void UpdateMusicVolume([NotNull] Slider slider) => UpdateMusicVolume(slider.value);

        public void UpdateMusicVolume(float volume)
        {
            foreach (var source in _music)
            {
                source.volume = volume;
            }
        }
    }
}
