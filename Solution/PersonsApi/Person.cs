using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PersonsApi
{
  public class Person
  {
    public string Name { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Gender Gender { get; set; }

    public int Age { get; set; }
    public Pet[] Pets { get; set; }
  }
}