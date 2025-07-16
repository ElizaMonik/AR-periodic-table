using System.Globalization;
using Schemas;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ui.Elements
{
    [UxmlElement]
    public partial class ChemicalElementCard : VisualElement
    {
        public Label SymbolLabel => this.Q<Label>("ElementSymbol");
        public Label NameLabel => this.Q<Label>("ElementName");
        public Label MassLabel => this.Q<Label>("ElementMass");
        public Label NumberLabel => this.Q<Label>("ElementNumber");
        public VisualElement Background => this.Q<VisualElement>("Element");

        public ChemicalElementCard() : this("Xx", "Unknown", 0f, 0, "#FFFFFF")
        {
        }

        public ChemicalElementCard(ChemicalElement element) : this(element.Symbol, element.Name, element.Mass, element.Number, element.Color)
        {
        }

        public ChemicalElementCard(string symbol, string name, float mass, int number, string hexColor)
        {
            var template = Resources.Load<VisualTreeAsset>("Components/ChemicalElementCard");
            template.CloneTree(this);

            SymbolLabel.text = symbol;
            NameLabel.text = name;
            MassLabel.text = mass.ToString(CultureInfo.InvariantCulture);
            NumberLabel.text = number.ToString(CultureInfo.InvariantCulture);
            if (ColorUtility.TryParseHtmlString(hexColor, out var color))
            {
                Background.style.backgroundColor = color;

                var upLeftBorderColor = color * 0.8f; // Darken the color by multiplying by a factor
                var downRightBorderColor = color * 1.2f; // Lighten the color by multiplying by a factor

                Background.style.borderTopColor = upLeftBorderColor;
                Background.style.borderLeftColor = upLeftBorderColor;
                Background.style.borderBottomColor = downRightBorderColor;
                Background.style.borderRightColor = downRightBorderColor;
            }
            else
            {
                Debug.LogWarning($"Invalid color string: {hexColor}. Using default color.");
                Background.style.backgroundColor = Color.white; // Default color if parsing fails
            }
        }
    }
}