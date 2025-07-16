using Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Ui
{
    [RequireComponent(typeof(UIDocument))]
    public class MenuScreen : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private Button _periodicTableButton;
        private Button _startButton;
        private Button _fusionButton;
        private Button _creditsButton;
        private Button _exitButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            Debug.Log("MainMenuController enabled");
            var root = _uiDocument.rootVisualElement;

            _periodicTableButton = root.Q<Button>("periodicTableButton");
            _startButton = root.Q<Button>("startButton");
            _fusionButton = root.Q<Button>("fusionButton");
            _creditsButton = root.Q<Button>("creditsButton");
            _exitButton = root.Q<Button>("exitButton");

            _periodicTableButton.clicked += OnPeriodicTableButtonClicked;
            _startButton.clicked += OnStartButtonClicked;
            _fusionButton.clicked += OnFusionButtonClicked;
            _creditsButton.clicked += OnCreditsButtonClicked;
            _exitButton.clicked += OnExitButtonClicked;
        }

        private void OnDisable()
        {
            Debug.Log("MainMenuController disabled");

            _periodicTableButton.clicked -= OnPeriodicTableButtonClicked;
            _startButton.clicked -= OnStartButtonClicked;
            _fusionButton.clicked -= OnFusionButtonClicked;
            _creditsButton.clicked -= OnCreditsButtonClicked;
            _exitButton.clicked -= OnExitButtonClicked;
        }

        private void OnPeriodicTableButtonClicked()
        {
            Debug.Log("Periodic Table button clicked");
            UIManager.SwitchUI<PeriodicTableScreen>(this);
        }

        private static void OnStartButtonClicked()
        {
            Debug.Log("Start button clicked");
            MoleculesController.CurrentMolecule = null; // Reset current molecule
            SceneManager.LoadScene("GameScene");
        }

        private void OnFusionButtonClicked()
        {
            Debug.Log("Settings button clicked");
            UIManager.SwitchUI<MoleculesScreen>(this);
        }

        private void OnCreditsButtonClicked()
        {
            Debug.Log("Credits button clicked");
            UIManager.SwitchUI<AboutUsScreen>(this);
        }


        private static void OnExitButtonClicked()
        {
            Debug.Log("Exit button clicked");
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}