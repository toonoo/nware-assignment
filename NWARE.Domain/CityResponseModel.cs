using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NWARE.Domain
{
    public class CityResponseModel
    {
        [JsonPropertyName("City Name")]
        public string CityName { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonPropertyName("Population")]
        public double Population { get; set; }
    }
}
