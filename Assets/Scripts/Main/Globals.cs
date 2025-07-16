using System.Collections.Generic;
using Newtonsoft.Json;
using Schemas;
using UnityEngine;
using UnityEngine.UIElements;

namespace Main
{
    public static class Globals
    {
        private static Material QuadTemplateMaterial { get; set; }
        private static PanelSettings QuadPanelSettings { get; set; }


        public static PanelSettings InstantiateQuadPanelSettings()
        {
            var panel = ScriptableObject.CreateInstance<PanelSettings>();
            panel.themeStyleSheet = QuadPanelSettings.themeStyleSheet;
            panel.scaleMode = QuadPanelSettings.scaleMode;
            return panel;
        }

        public static Material InstantiateQuadTemplateMaterial()
        {
            return new Material(QuadTemplateMaterial);
        }

        public static List<ChemicalElement> Elements { get; private set; }
        public static List<Molecule> Molecules { get; private set; }

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            Debug.Log("Globals initialized");
            Debug.Log("Loading elements...");
            QuadTemplateMaterial = Resources.Load<Material>("UIDocument/Quad/Material");
            QuadPanelSettings = Resources.Load<PanelSettings>("UIDocument/Quad/PanelSettings");

            var elementsJson = Resources.Load<TextAsset>("Data/Elements"); // sin .json
            Elements = JsonConvert.DeserializeObject<List<ChemicalElement>>(elementsJson.text);
            Debug.Log("Elements loaded: " + Elements.Count);

            var moleculesJson = Resources.Load<TextAsset>("Data/Molecules"); // sin .json
            Molecules = JsonConvert.DeserializeObject<List<Molecule>>(moleculesJson.text);
            Debug.Log("Molecules loaded: " + Molecules.Count);

            foreach (var molecule in Molecules)
            {
                molecule.ModelResult = Resources.Load<GameObject>($"Molecules/{molecule.Name}/{molecule.Name}");
            }
        }
    }
}