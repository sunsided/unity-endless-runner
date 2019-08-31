using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject[] panels;
        public GameObject[] buttons;

        private GameObject _openPanel;

        private int _maxLives = 3;

        public void LoadGameScene()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.Lives, _maxLives);
            GameData.Singleton.ResetScore();
            SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);
        }

        [ContractAnnotation("=>halt")]
        public void QuitGame() => Application.Quit();

        public void OpenPanel([NotNull] GameObject panel)
        {
            Debug.Log($"Opening panel {panel}");
            _openPanel = panel;
            panel.SetActive(true);
            EnableAllMainButtons(false);
        }

        public void ClosePanel([NotNull] Button button)
        {
            var panel = button.gameObject.transform.parent.gameObject;
            Debug.Assert(panel == _openPanel, "panel == _openPanel");
            _openPanel = null;
            ClosePanel(panel);
            EnableAllMainButtons();
        }

        private static void ClosePanel([NotNull] GameObject panel)
        {
            Debug.Log($"Closing panel {panel}");
            panel.SetActive(false);
        }

        private void Start()
        {
            buttons = GameObject.FindGameObjectsWithTag("MainMenuButton");
            panels = GameObject.FindGameObjectsWithTag("MainMenuSubPanel");
            Debug.Log($"Found {panels.Length} panels and {buttons.Length} buttons.");

            foreach (var panel in panels)
            {
                ClosePanel(panel);
            }

            EnableAllMainButtons();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel")) HandleCancelButton();
        }

        private void HandleCancelButton()
        {
            if (_openPanel != null && _openPanel.activeSelf)
            {
                ClosePanel(_openPanel);
                EnableAllMainButtons();
            }
            else
            {
                QuitGame();
            }
        }

        private void EnableAllMainButtons(bool enabled = true)
        {
            foreach (var b in buttons)
            {
                b.SetActive(enabled);
            }
        }
    }
}
