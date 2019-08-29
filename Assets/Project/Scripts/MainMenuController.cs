using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void CloseHelpPanel() => ClosePanel(helpPanel);

        public void ShowOptionsPanel() => OpenPanel(optionsPanel);

        public void CloseOptionsPanel() => ClosePanel(optionsPanel);

        public void ShowStatisticsPanel() => OpenPanel(statisticsPanel);

        public void CloseStatisticsPanel() => ClosePanel(statisticsPanel);

        private static void OpenPanel([NotNull] GameObject panel) => panel.SetActive(true);

        private static void ClosePanel([NotNull] GameObject panel) => panel.SetActive(false);

        private void Start()
        {
            CloseHelpPanel();
            CloseOptionsPanel();
            CloseStatisticsPanel();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel")) HandleCancelButton();
        }

        private void HandleCancelButton()
        {
            if (helpPanel.activeSelf)
            {
                CloseHelpPanel();
            }
            else if (optionsPanel.activeSelf)
            {
                CloseOptionsPanel();
            }
            else if (statisticsPanel.activeSelf)
            {
                CloseStatisticsPanel();
            }
            else
            {
                QuitGame();
            }
        }
    }
}
