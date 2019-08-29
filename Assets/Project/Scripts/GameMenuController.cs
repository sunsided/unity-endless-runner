using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class GameMenuController : MonoBehaviour
    {
        public void LoadMenuScene()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
