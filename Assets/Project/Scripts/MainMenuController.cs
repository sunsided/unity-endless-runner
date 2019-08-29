using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
