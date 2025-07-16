using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Ui
{
    [RequireComponent(typeof(UIDocument))]
    public class GameController : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private Button _mainMenuButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            if (_uiDocument == null)
            {
                Debug.LogError("UIDocument component not found on this GameObject.");
            }
        }

        private void OnEnable()
        {
            var root = _uiDocument.rootVisualElement;

            _mainMenuButton = root.Q<Button>("mainMenuButton");

            _mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuButtonClicked);
        }

        private void OnDisable()
        {
            _mainMenuButton.UnregisterCallback<ClickEvent>(OnMainMenuButtonClicked);
        }

        private static void OnMainMenuButtonClicked(ClickEvent evt)
        {
            Debug.Log("Start button clicked");
            SceneManager.LoadScene("MenuScene");
        }
    }
}