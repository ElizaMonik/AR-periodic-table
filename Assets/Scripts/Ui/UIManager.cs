using UnityEngine;

namespace Ui
{
    public static class UIManager
    {
        public static void SwitchUI<T>(MonoBehaviour currentUi) where T : MonoBehaviour
        {
            // Deactivate all UI elements
            var newUi = Object.FindFirstObjectByType<T>(FindObjectsInactive.Include);
            if (newUi == null)
            {
                Debug.LogError($"UI of type {typeof(T)} not found.");
                return;
            }

            newUi.gameObject.SetActive(true);
            currentUi.gameObject.SetActive(false);
        }
    }
}