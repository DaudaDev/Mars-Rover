using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarsRover.Domain.Entities.Rovers;

[JsonConverter(typeof(StringEnumConverter))]
public enum Direction
{
    N,
    S,
    E,
    W
}