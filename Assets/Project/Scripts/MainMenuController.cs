using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject helpPanel;

        public void LoadGameScene()
        {
            SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);
        }

        [ContractAnnotation("=>halt")]
        public void QuitGame()
        {
            if (helpPanel.activeSelf)
            {
                CloseHelpPanel();
            }
            else
            {
                Application.Quit();
            }
        }

        public void ShowHelpPanel()
        {
            helpPanel.SetActive(true);
        }

        public void CloseHelpPanel()
        {
            helpPanel.SetActive(false);
        }

        private void Start()
        {
            CloseHelpPanel();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel")) QuitGame();
        }
    }
}
