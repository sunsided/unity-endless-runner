using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class UpdateSFX : MonoBehaviour
    {
        public float defaultVolume = 0.5f;

        private Slider _slider;
        private GameData _gameData;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void Start()
        {
            _gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();

            Debug.Assert(PlayerPrefs.HasKey(PlayerPrefKeys.SoundVolume), "PlayerPrefs.HasKey(PlayerPrefKeys.SoundVolume)");
            var storedValue = PlayerPrefs.GetFloat(PlayerPrefKeys.SoundVolume);
            _slider.value = storedValue;

            UpdateSoundVolume();
        }

        public void UpdateSoundVolume() => _gameData.UpdateSoundVolume(_slider.value);
    }
}
