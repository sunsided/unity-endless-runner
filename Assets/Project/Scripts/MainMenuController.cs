using System;
using JetBrains.Annotations;
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

        [ContractAnnotation("=>halt")]
        public void QuitGame()
        {
            Application.Quit();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel")) QuitGame();
        }
    }
}
