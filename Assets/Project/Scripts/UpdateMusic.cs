using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class UpdateMusic : MonoBehaviour
    {
        private Slider _slider;
        private GameData _gameData;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void Start()
        {
            _gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();

            Debug.Assert(PlayerPrefs.HasKey(PlayerPrefKeys.MusicVolume), "PlayerPrefs.HasKey(PlayerPrefKeys.MusicVolume)");
            var storedValue = PlayerPrefs.GetFloat(PlayerPrefKeys.MusicVolume);
            _slider.value = storedValue;

            UpdateMusicVolume();
        }

        public void UpdateMusicVolume() => _gameData.UpdateMusicVolume(_slider.value);


    }
}
