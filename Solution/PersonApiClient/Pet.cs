using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PersonsApi
{
  public class Pet
  {
    [JsonConverter(typeof(StringEnumConverter))]
    public PetType Type { get; set; }
    public string Name { get; set; }
  }
}