using System.Collections.Generic;
using System.Linq;
using Schemas;
using Ui.Elements;
using UnityEngine;
using Utils;

namespace Components
{
    public class MoleculesController : MonoBehaviour
    {
        public static Molecule CurrentMolecule { get; set; }

        [SerializeField] private List<AtomicModel> atomicModels;
        [SerializeField] private GameObject molecule;
        [SerializeField] private float rotationSpeed = 50f; // Velocidad de rotación en grados por segundo

        public GameObject Molecule => molecule;
        public GameObject Quad { get; private set; }

        private void Update()
        {
            if (molecule == null) return;

            var positions = atomicModels
                .Where(am => am.gameObject.activeInHierarchy)
                .Select(am => am.transform.position)
                .ToArray();

            if (positions.Length == 0) return;
            var center = Vector3Utils.GetCenter(positions);
            molecule.transform.position = center;
            molecule.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        public void RegisterForMolecule(AtomicModel atomicModel)
        {
            if (atomicModels.Contains(atomicModel))
            {
                return;
            }

            atomicModels.Add(atomicModel);

            Debug.Log($"Atomic model {atomicModel.ElementSymbol} added to fusion list.");
            if (atomicModels.Count >= 2)
            {
                TryCreateMolecule();
            }
        }

        public void UnregisterForMolecule(AtomicModel atomicModel)
        {
            if (!atomicModels.Contains(atomicModel))
            {
                return;
            }

            if (!atomicModels.Remove(atomicModel))
            {
                return;
            }

            Debug.Log($"Atomic model {atomicModel.ElementSymbol} removed from fusion list.");
            TryRemoveMolecule();
        }

        private void TryCreateMolecule()
        {
            if (CurrentMolecule == null)
            {
                Debug.Log("A molecule is already being displayed, skipping fusion creation.");
                return;
            }

            if (!CurrentMolecule.HasCompatible(atomicModels))
            {
                Debug.Log("Current molecule is not compatible with the atomic models, skipping fusion creation.");
                return;
            }

            Debug.Log($"Trying to create molecule: {CurrentMolecule.Name}");
            if (molecule != null)
            {
                Destroy(molecule);
            }

            var modelResult = CurrentMolecule.ModelResult;
            Debug.Log($"Instantiating fusion model: {modelResult.name}: Position: {modelResult.transform.position}, Rotation: {modelResult.transform.rotation}");

            molecule = Instantiate(
                modelResult,
                modelResult.transform.position,
                modelResult.transform.rotation,
                transform
            );

            molecule.transform.localScale *= 0.03f;
            molecule.name = CurrentMolecule.Name;

            Quad = ElementUtils.CreateQuadUI(
                new MoleculeCard(CurrentMolecule.ComunName, CurrentMolecule.Description),
                molecule.transform
            );

            Quad.transform.localPosition = Vector3.up * 1.2f;
            Quad.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Quad.transform.localScale = Vector3.one;

            atomicModels.ForEach(a => a.SetEnabled(false));
        }

        private void TryRemoveMolecule()
        {
            if (molecule == null)
            {
                return;
            }

            Debug.Log("Removing fusion...");
            Destroy(molecule);
            molecule = null;
            atomicModels.ForEach(a => a.SetEnabled(true));
        }
    }
}