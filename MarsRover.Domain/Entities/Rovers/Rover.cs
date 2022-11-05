using System;
using System.Collections.Generic;

namespace MarsRover.Domain.Entities.Rovers;

public class Rover
{
    private readonly List<RoverPosition> _previousPositions = new();
    public Rover(RoverPosition currentRoverPosition)
    {
        CurrentRoverPosition = currentRoverPosition;
        _previousPositions.Add(currentRoverPosition);
    }

    public RoverPosition CurrentRoverPosition { get; private set; }
    public IReadOnlyCollection<RoverPosition> PreviousPositions => _previousPositions;

    public void SpinLeft()
    {
        
        var newDirection = CurrentRoverPosition.Direction switch
        {
            Direction.E => Direction.N,
            Direction.N => Direction.W,
            Direction.W => Direction.S,
            Direction.S => Direction.E,
            _ => throw new ArgumentOutOfRangeException()
        };

        CurrentRoverPosition = CurrentRoverPosition with { Direction = newDirection };
        _previousPositions.Add(CurrentRoverPosition);
    }
    
    public void SpinRight()
    {
        var newDirection = CurrentRoverPosition.Direction switch
        {
            Direction.E => Direction.S,
            Direction.S => Direction.W,
            Direction.W => Direction.N,
            Direction.N => Direction.E,
            _ => throw new ArgumentOutOfRangeException()
        };

        CurrentRoverPosition = CurrentRoverPosition with { Direction = newDirection };
        _previousPositions.Add(CurrentRoverPosition);
    }
    
    public void MoveForward(int x, int y)
    {
        CurrentRoverPosition = CurrentRoverPosition with { XAxis = x, YAxis = y };
        _previousPositions.Add(CurrentRoverPosition);
    }
}