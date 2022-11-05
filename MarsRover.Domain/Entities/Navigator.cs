using System;
using System.IO;
using System.Linq;
using MarsRover.Domain.Entities.Parsers;
using MarsRover.Domain.Entities.Plateaus;
using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Entities;

public class Navigator
{
    public ResultDto GetNavigations(Stream inputStream)
    {
        var parser = new Parser();
        var navigatorCommands = parser.GetNavigationCommands(inputStream);
        
        //The exercise says we should assume size to be 5,5.
        var plateau = new Plateau(5, 5);
        
        var rovers = navigatorCommands
            .Select(navigatorCommand => PlanRoverMovement(navigatorCommand, plateau))
            .ToList();

        return new ResultDto(
            Plateau: new PlateauDto(plateau.UpperXAxis, plateau.UpperYAxis),
            Rovers: rovers.Select(r => new RoverDto(r.PreviousPositions)).ToList());
    }

    private static Rover PlanRoverMovement(NavigatorCommand navigatorCommand, Plateau plateau)
    {
        var roverPosition = new RoverPosition(
            navigatorCommand.XAxis,
            navigatorCommand.YAxis,
            navigatorCommand.Direction);
        
        var rover = new Rover(roverPosition);
        
        foreach (var movement in navigatorCommand.Movements)
        {
            MoveRover(plateau, movement, rover);
        }

        return rover;
    }

    private static void MoveRover(Plateau plateau, Movement movement, Rover rover)
    {
        switch (movement)
        {
            case Movement.L:
                rover.SpinLeft();
                break;
            case Movement.R:
                rover.SpinRight();
                break;
            case Movement.M:
            {
                var newPosition = plateau.GetNextRoverPosition(rover.CurrentRoverPosition);
                rover.MoveForward(newPosition.XAxis, newPosition.YAxis);
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}