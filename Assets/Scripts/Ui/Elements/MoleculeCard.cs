using UnityEngine;
using UnityEngine.UIElements;

namespace Ui.Elements
{
    [UxmlElement]
    public partial class MoleculeCard : VisualElement
    {
        public Label MoleculeName => this.Q<Label>("MoleculeName");
        public Label MoleculeDescription => this.Q<Label>("MoleculeDescription");

        public MoleculeCard() : this("Xx", "Unknown")
        {
        }

        public MoleculeCard(string moleculeName, string moleculeDescription)
        {
            var template = Resources.Load<VisualTreeAsset>("Components/MoleculeCard");
            template.CloneTree(this);

            MoleculeName.text = moleculeName;
            MoleculeDescription.text = moleculeDescription;
        }
    }
}