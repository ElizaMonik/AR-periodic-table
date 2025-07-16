using System.Collections.Generic;
using Schemas;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ui.Elements
{
    [UxmlElement]
    public partial class MoleculeItem : VisualElement
    {
        public Label NameLabel => this.Q<Label>("MoleculeName");
        public Label ComunNameLabel => this.Q<Label>("MoleculeComunName");
        public Label ChemicalElementsLabel => this.Q<Label>("ChemicalElements");
        public Label DescriptionLabel => this.Q<Label>("MoleculeDescription");

        public MoleculeItem() : this("Unknown Molecule", new List<string>(), "Unknown", "No description available")
        {
        }

        public MoleculeItem(Molecule molecule) : this(
            molecule.Name,
            molecule.Elements,
            molecule.ComunName,
            molecule.Description)
        {
        }

        public MoleculeItem(string name, List<string> elements, string comunName, string description)
        {
            var template = Resources.Load<VisualTreeAsset>("Components/MoleculeItem");
            template.CloneTree(this);

            NameLabel.text = name;
            ComunNameLabel.text = comunName;
            ChemicalElementsLabel.text = string.Join(" + ", elements);
            DescriptionLabel.text = description;
        }
    }
}