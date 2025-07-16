using System;
using System.Collections;
using System.Collections.Generic;
using Http;
using Main;
using Siccity.GLTFUtility;
using Ui.Elements;
using UnityEngine;
using Utils;

namespace Components
{
    [RequireComponent(typeof(DefaultObserverEventHandler))]
    public class AtomicModel : MonoBehaviour
    {
        public string ElementSymbol { get; private set; }
        private ChemicalElementCard ElementCard { get; set; }
        private GameObject ChemicalElement { get; set; }
        private GameObject Quad { get; set; }

        [SerializeField] private Renderer[] renderers;

        private static readonly ISet<AtomicModel> Tracked = new HashSet<AtomicModel>();

        private DefaultObserverEventHandler _observerEventHandler;
        private MoleculesController _moleculesController;

        private readonly ImportSettings _importSettings = new()
        {
            animationSettings = new AnimationSettings
            {
                useLegacyClips = true,
                looping = true
            }
        };

        public static IEnumerable<AtomicModel> TrackedModels => Tracked;

        public void Awake()
        {
            _moleculesController = FindAnyObjectByType<MoleculesController>();
            _observerEventHandler = GetComponent<DefaultObserverEventHandler>();
            if (_observerEventHandler == null)
            {
                Debug.LogError("DefaultObserverEventHandler component is missing on AtomicModel.");
                return;
            }

            _observerEventHandler.OnTargetFound.AddListener(RegisterForFusion);
            _observerEventHandler.OnTargetLost.AddListener(UnregisterForFusion);
        }

        private void Start()
        {
            ElementSymbol = gameObject.name;
            var element = Globals.Elements.Find(e => e.Symbol == ElementSymbol);
            if (element == null)
            {
                Debug.LogError($"Element with symbol {ElementSymbol} not found.");
            }

            ElementCard = element != null ? new ChemicalElementCard(element) : new ChemicalElementCard();
        }

        public void ShowModel()
        {
            try
            {
                Debug.Log("Showing model...");
                if (ChemicalElement != null)
                {
                    Tracked.Add(this);
                    ChemicalElement.SetActive(true);
                    var anim = ChemicalElement.GetComponent<Animation>();
                    foreach (AnimationState state in anim)
                    {
                        anim.Play(state.name);
                    }
                }
                else
                {
                    FetchModel();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void HideModel()
        {
            Debug.Log("Hiding model...");
            if (ChemicalElement == null) return;
            Tracked.Remove(this);
            ChemicalElement.SetActive(false);
        }

        private void FetchModel()
        {
            var request = WebRequests.Get<byte[]>($"https://models.periodic-table-ar.lat/model/{ElementSymbol}")
                .Then(data =>
                {
                    Debug.Log($"Request succeeded, model loaded: {data.Length} bytes");
                    ChemicalElement = Importer.LoadFromBytes(data, _importSettings, out var animationClips);
                    Debug.Log($"Model loaded: {ChemicalElement.name}, animations: {animationClips.Length}");

                    renderers = ChemicalElement.GetComponentsInChildren<Renderer>();

                    SetChemicalElementTransforms();
                    CreateQuadWithUiTexture();
                    AddBoxTrigger();

                    var anim = ChemicalElement.AddComponent<Animation>();
                    foreach (var clip in animationClips)
                    {
                        Debug.Log($"Processing clip: {clip.name}, length: {clip.length}s");
                        anim.AddClip(clip, clip.name);
                        anim.Play(clip.name);
                    }

                    Tracked.Add(this);
                })
                .Catch(Debug.LogException)
                .Finally(() => Debug.Log("Request finished, All done"));

            StartCoroutine(request.Send());
        }

        //
        // Sets the transforms for the chemical element model
        //
        private void SetChemicalElementTransforms()
        {
            Debug.Log("Updating transforms for atomic model: " + ElementSymbol);
            ChemicalElement.transform.SetParent(transform, false); // false mantiene la posición world
            ChemicalElement.transform.localPosition = Vector3.zero;
            ChemicalElement.transform.localRotation = Quaternion.Euler(Vector3.zero);
            ChemicalElement.transform.localScale = Vector3.one * 0.25f;
        }

        //
        // Creates a quad with a UI texture for the atomic model
        //
        private void CreateQuadWithUiTexture()
        {
            Debug.Log("Adding quad texture for atomic model: " + ElementSymbol);
            Quad = ElementUtils.CreateQuadUI(ElementCard, ChemicalElement.transform);
            Quad.transform.localPosition = Vector3.up * 0.3f;
            Quad.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Quad.transform.localScale = Vector3.one * 0.30f;

            StartCoroutine(DelayedSetActive());
            return;

            IEnumerator DelayedSetActive()
            {
                yield return null; // Espera un frame
                if (_moleculesController.Molecule != null)
                {
                    SetEnabled(false);
                }
            }
        }

        //
        // Adds a box collider trigger to the atomic model
        //
        private void AddBoxTrigger()
        {
            Debug.Log("Adding box collider trigger for atomic model: " + ElementSymbol);
            var boxCollider = ChemicalElement.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.size = Vector3.one * 0.7f;
        }


        public void SetEnabled(bool enable)
        {
            ChemicalElement?.SetActive(enable);
            // if (renderers == null || renderers.Length == 0)
            // {
            //     Debug.LogWarning("No renderers found to toggle.");
            //     return;
            // }
            //
            // foreach (var r in renderers)
            // {
            //     r.enabled = enable;
            // }
            //
            // Quad?.SetActive(enable);
        }


        private void RegisterForFusion()
        {
            _moleculesController.RegisterForMolecule(this);
        }

        private void UnregisterForFusion()
        {
            _moleculesController.UnregisterForMolecule(this);
        }
    }
}