using System.Collections.Generic;
using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Entities;

public record ResultDto(PlateauDto Plateau, IReadOnlyCollection<RoverDto> Rovers);

public record RoverDto(IReadOnlyCollection<RoverPosition> previousMovements);

public record PlateauDto(int UpperX, int UpperY);