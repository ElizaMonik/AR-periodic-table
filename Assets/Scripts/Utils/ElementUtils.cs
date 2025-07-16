using Main;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utils
{
    public static class ElementUtils
    {
        public static GameObject CreateQuadUI(
            VisualElement visualElement,
            Transform parent
        )
        {
            //LOAD REQUIRED RESOURCES
            visualElement.style.width = new Length(100, LengthUnit.Percent);
            visualElement.style.height = new Length(100, LengthUnit.Percent);

            var texture = new RenderTexture(200, 244, 0, RenderTextureFormat.ARGB32)
            {
                filterMode = FilterMode.Bilinear,
                useMipMap = false,
                autoGenerateMips = false
            };

            var panelSettings = Globals.InstantiateQuadPanelSettings();
            panelSettings.targetTexture = texture;

            var material = Globals.InstantiateQuadTemplateMaterial();
            material.mainTextureOffset = Vector2.zero;
            material.mainTextureScale = Vector2.one;
            material.mainTexture = texture;

            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.transform.SetParent(parent, false);

            var rendered = quad.GetComponent<MeshRenderer>();
            rendered.material = material;

            var document = quad.AddComponent<UIDocument>();
            document.panelSettings = panelSettings;
            var root = document.rootVisualElement;
            root.Add(visualElement);

            return quad;
        }
    }
}