using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class RegisterScore : MonoBehaviour
    {
        private void Update()
        {
            if (GameData.Singleton == null) return;
            GameData.Singleton.scoreText = GetComponent<Text>();
            GameData.Singleton.AddScore(0);
        }
    }
}
