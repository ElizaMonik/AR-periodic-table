using Newtonsoft.Json;

namespace Schemas
{
    public class ChemicalElement
    {
        [JsonProperty("symbol")] public string Symbol { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("mass")] public float Mass { get; set; }

        [JsonProperty("number")] public int Number { get; set; }

        [JsonProperty("color")] public string Color { get; set; }
    }
}