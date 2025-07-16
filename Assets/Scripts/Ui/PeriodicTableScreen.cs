using UnityEngine;
using UnityEngine.UIElements;

namespace Ui
{
    [RequireComponent(typeof(UIDocument))]
    public class PeriodicTableScreen : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private Button _menuButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            Debug.Log("PeriodicTableController enabled");
            var root = _uiDocument.rootVisualElement;

            _menuButton = root.Q<Button>("menuButton");
            _menuButton.clicked += OnMenuButtonClicked;
        }

        private void OnDisable()
        {
            Debug.Log("PeriodicTableController disabled");
            _menuButton.clicked -= OnMenuButtonClicked;
        }


        private void OnMenuButtonClicked()
        {
            UIManager.SwitchUI<MenuScreen>(this);
        }
    }
}