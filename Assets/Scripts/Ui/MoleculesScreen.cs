using System.Linq;
using Components;
using Main;
using Ui.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Ui
{
    [RequireComponent(typeof(UIDocument))]
    public class MoleculesScreen : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private Button _menuButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            if (_uiDocument == null)
            {
                Debug.LogError("UIDocument component is missing on FusionController.");
                return;
            }

            var root = _uiDocument.rootVisualElement;
            var moleculesContainers = root.Q<VisualElement>("moleculesContainer");
            foreach (var molecule in Globals.Molecules)
            {
                var element = new MoleculeItem(molecule);
                moleculesContainers.Add(element);


                element.RegisterCallback<ClickEvent>(_ =>
                {
                    MoleculesController.CurrentMolecule = molecule;
                    SceneManager.LoadScene("GameScene");
                });
            }
        }

        private void OnEnable()
        {
            Debug.Log("FusionController enabled");
            var root = _uiDocument.rootVisualElement;

            _menuButton = root.Q<Button>("menuButton");
            _menuButton.clicked += OnMenuButtonClicked;
        }

        private void OnDisable()
        {
            Debug.Log("FusionController disabled");
            _menuButton.clicked -= OnMenuButtonClicked;
        }


        private void OnMenuButtonClicked()
        {
            UIManager.SwitchUI<MenuScreen>(this);
        }
    }
}