using System;
using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Entities.Plateaus;

public sealed class Plateau
{
    public Plateau(
        int upperXAxis,
        int upperYAxis,
        bool throwErrorWhenOutBounds = false)
    {
        ThrowErrorWhenOutBounds = throwErrorWhenOutBounds;
        SetUpperX(upperXAxis);
        SetUpperY(upperYAxis);
        LowerXAxis = 0;
        LowerYAxis = 0;
    }

    public int UpperXAxis { get; private set; }
    public int UpperYAxis { get; private set; }

    public int LowerXAxis { get;  }
    public int LowerYAxis { get; }
    
    //Adding this for edge cases where the rover goes out of the plateau
    //Given more time i would display thing information better in the front end
    public bool ThrowErrorWhenOutBounds { get; }

    public RoverPosition GetNextRoverPosition(RoverPosition currentPosition)
    {
        var (newXAxis, newYAxis) = currentPosition.Direction switch
        {
            Direction.E => (currentPosition.XAxis + 1, currentPosition.YAxis),
            Direction.S => (currentPosition.XAxis, currentPosition.YAxis - 1),
            Direction.W => (currentPosition.XAxis - 1, currentPosition.YAxis),
            Direction.N => (currentPosition.XAxis, currentPosition.YAxis+1),
            _ => throw new ArgumentOutOfRangeException()
        };

        newYAxis = GetNewYValue(newYAxis);
        newXAxis = GetNewXValue(newXAxis);

        return currentPosition with { XAxis = newXAxis, YAxis = newYAxis };
    }

    private int GetNewXValue(int newValue)
    {
        newValue = GetUpperValue(newValue, UpperXAxis);
        return GetLowerValue(newValue, LowerXAxis);
    }
    
    private int GetNewYValue(int newValue)
    {
        newValue = GetUpperValue(newValue, UpperYAxis);
        return GetLowerValue(newValue, LowerYAxis);
    }
    private int GetLowerValue(int newYAxis, int lowerValue)
    {
        if (newYAxis >= lowerValue) return newYAxis;
        if (ThrowErrorWhenOutBounds)
        {
            throw new PlateauOutOfBoundsException(
                $"Rover new position {newYAxis} cannot be less than the lower limit {lowerValue}");
        }

        newYAxis = lowerValue;

        return newYAxis;
    }

    private int GetUpperValue(int newYAxis, int upperValue)
    {
        if (newYAxis <= upperValue) return newYAxis;
        if (ThrowErrorWhenOutBounds)
        {
            throw new PlateauOutOfBoundsException(
                $"Rover new position {newYAxis} cannot be greater than the upper limit {upperValue}");
        }

        newYAxis = upperValue;

        return newYAxis;
    }

    private void SetUpperX(int upperXAxis)
    {
        if (upperXAxis <= 0)
        {
            throw new ArgumentException("The Upper XAxis should be grater than 0");
        }
        
        UpperXAxis = upperXAxis;
    }
    
    private void SetUpperY(int upperYAxis)
    {
        if (upperYAxis <= 0)
        {
            throw new ArgumentException("The Upper YAxis should be grater than 0");
        }
        
        UpperYAxis = upperYAxis;
    }
}