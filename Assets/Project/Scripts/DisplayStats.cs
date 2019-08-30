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
            if (PlayerPrefs.HasKey("lastScore"))
            {
                lastScore.text = $"Last score: {PlayerPrefs.GetInt("lastScore")}";
            }
            else
            {
                lastScore.text = "Last score: 0";
            }

            if (PlayerPrefs.HasKey("highscore"))
            {
                highestScore.text = $"Highest score: {PlayerPrefs.GetInt("highscore")}";
            }
            else
            {
                highestScore.text = "Highest score: 0";
            }
        }
    }
}
