using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class RegisterHighscore : MonoBehaviour
    {
        private void Update()
        {
            if (GameData.Singleton == null) return;
            GameData.Singleton.highscoreText = GetComponent<Text>();
            GameData.Singleton.AddScore(0);
        }
    }
}