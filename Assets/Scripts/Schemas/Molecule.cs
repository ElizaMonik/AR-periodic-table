using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Newtonsoft.Json;
using UnityEngine;

namespace Schemas
{
    public class Molecule
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("elements")] public List<string> Elements { get; set; } = new();
        [JsonProperty("comunName")] public string ComunName { get; set; }
        [JsonProperty("description")] public string Description { get; set; }

        public GameObject ModelResult { get; set; }

        public bool HasCompatible(List<AtomicModel> atomicModels)
        {
            var models = atomicModels.ToList();
            foreach (var element in Elements)
            {
                var model = PopIf(models, m => m.ElementSymbol == element);
                if (model == null)
                {
                    return false;
                }
            }

            return models.Count == 0;
        }

        private static AtomicModel PopIf(List<AtomicModel> models, Func<AtomicModel, bool> predicate)
        {
            var model = models.FirstOrDefault(predicate);
            if (model != null) models.Remove(model);
            return model;
        }
    }
}