using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class DisplayStats : MonoBehaviour
    {
        public Text lastScore;
        public Text highestScore;

        private void OnEnable()
        {
            if (PlayerPrefs.HasKey(PlayerPrefKeys.LastScore))
            {
                lastScore.text = $"Last score: {PlayerPrefs.GetInt(PlayerPrefKeys.LastScore)}";
            }
            else
            {
                lastScore.text = "Last score: 0";
            }

            if (PlayerPrefs.HasKey(PlayerPrefKeys.Highscore))
            {
                highestScore.text = $"Highest score: {PlayerPrefs.GetInt(PlayerPrefKeys.Highscore)}";
            }
            else
            {
                highestScore.text = "Highest score: 0";
            }
        }
    }
}
