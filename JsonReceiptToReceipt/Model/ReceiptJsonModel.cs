using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonReceiptToReceipt.Model
{
    public class PartOfReceipt
    {
        [JsonPropertyName("locale")]
        public string Locale { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("boundingPoly")]
        public BoundingPoly BoundingPoly { get; set; }
    }

    public class BoundingPoly
    {
        [JsonPropertyName("vertices")]
        public List<Vertex> Vertices { get; set; }
    }

    public class Vertex
    {
        [JsonPropertyName("x")]
        public int X { get; set; }
        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
