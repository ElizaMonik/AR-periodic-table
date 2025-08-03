using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ui
{
    [RequireComponent(typeof(UIDocument))]
    public class PeriodicTableScreen : MonoBehaviour
    {
        private VisualElement _periodicTable;
        private UIDocument _uiDocument;
        private Button _menuButton;

        private float _zoom = 1f;
        private float _minZoom = 0.5f;
        private float _maxZoom = 3f;
        private float? _lastPinchDistance;

        private readonly Dictionary<int, Vector2> _activeTouches = new();
        private readonly Dictionary<int, Vector2> _initialTouchPositions = new();
        private Vector2 _initialElementPosition;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            _activeTouches[evt.pointerId] = evt.position;
            _initialTouchPositions[evt.pointerId] = evt.position;

            // Guarda la posición inicial del elemento cuando comienza el primer toque
            if (_activeTouches.Count == 1)
            {
                _initialElementPosition = new Vector2(
                    _periodicTable.resolvedStyle.left,
                    _periodicTable.resolvedStyle.top
                );
            }

            _periodicTable.CapturePointer(evt.pointerId);
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            if (!_activeTouches.ContainsKey(evt.pointerId)) return;

            _activeTouches[evt.pointerId] = evt.position;

            switch (_activeTouches.Count)
            {
                case 1:
                {
                    // Calcula el desplazamiento relativo desde la posición inicial del toque
                    var currentTouchPosition = _activeTouches[evt.pointerId];
                    var initialTouchPosition = _initialTouchPositions[evt.pointerId];
                    var deltaPosition = currentTouchPosition - initialTouchPosition;

                    // Aplica el desplazamiento a la posición inicial del elemento
                    _periodicTable.style.left = _initialElementPosition.x + deltaPosition.x;
                    _periodicTable.style.top = _initialElementPosition.y + deltaPosition.y;
                    break;
                }
                case 2:
                {
                    var touches = new List<Vector2>(_activeTouches.Values);
                    var currentDistance = Vector2.Distance(touches[0], touches[1]);

                    // Guarda la distancia previa entre los dos toques
                    if (!_lastPinchDistance.HasValue)
                    {
                        _lastPinchDistance = currentDistance;
                        return;
                    }

                    var delta = currentDistance - _lastPinchDistance.Value;

                    // Ajusta el zoom
                    _zoom += delta * 0.005f;
                    _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);

                    _periodicTable.style.scale = new StyleScale(new Scale(new Vector2(_zoom, _zoom)));

                    _lastPinchDistance = currentDistance;
                    break;
                }
            }
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            _activeTouches.Remove(evt.pointerId);
            _initialTouchPositions.Remove(evt.pointerId);
            _periodicTable.ReleasePointer(evt.pointerId);
            _lastPinchDistance = null;
        }

        private void OnEnable()
        {
            Debug.Log("PeriodicTableController enabled");
            var root = _uiDocument.rootVisualElement;

            _menuButton = root.Q<Button>("menuButton");
            _periodicTable = root.Q<VisualElement>("periodicTable");

            _menuButton.clicked += OnMenuButtonClicked;
            _periodicTable.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _periodicTable.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _periodicTable.RegisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnDisable()
        {
            Debug.Log("PeriodicTableController disabled");
            _menuButton.clicked -= OnMenuButtonClicked;
            _periodicTable.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            _periodicTable.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            _periodicTable.UnregisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnMenuButtonClicked()
        {
            UIManager.SwitchUI<MenuScreen>(this);
        }
    }
}