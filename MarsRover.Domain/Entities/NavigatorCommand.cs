using System.Collections.Generic;
using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Entities;

public record NavigatorCommand(string XAxis, string YAxis, string Direction, IReadOnlyCollection<Movement> Movements);