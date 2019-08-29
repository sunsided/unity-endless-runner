using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject helpPanel;
        public GameObject optionsPanel;
        public GameObject statisticsPanel;

        public void LoadGameScene()
        {
            SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);
        }

        [ContractAnnotation("=>halt")]
        public void QuitGame() => Application.Quit();

        public void ShowHelpPanel() => OpenPanel(helpPanel);

        public void ShowOptionsPanel() => OpenPanel(optionsPanel);

        public void ShowStatisticsPanel() => OpenPanel(statisticsPanel);

        public void ClosePanel([NotNull] Button button) => button.gameObject.transform.parent.gameObject.SetActive(false);

        private static void OpenPanel([NotNull] GameObject panel) => panel.SetActive(true);

        private static void ClosePanel([NotNull] GameObject panel) => panel.SetActive(false);

        private void Start()
        {
            ClosePanel(helpPanel);
            ClosePanel(optionsPanel);
            ClosePanel(statisticsPanel);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel")) HandleCancelButton();
        }

        private void HandleCancelButton()
        {
            if (helpPanel.activeSelf)
            {
                ClosePanel(helpPanel);
            }
            else if (optionsPanel.activeSelf)
            {
                ClosePanel(optionsPanel);
            }
            else if (statisticsPanel.activeSelf)
            {
                ClosePanel(statisticsPanel);
            }
            else
            {
                QuitGame();
            }
        }
    }
}
