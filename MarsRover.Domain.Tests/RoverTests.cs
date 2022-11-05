using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Tests;

public class RoverTests
{
    [Theory]
    [InlineData(1,2,"E", Direction.E)]
    [InlineData(12,23,"n", Direction.N)]
    [InlineData(11,29,"W", Direction.W)]
    [InlineData(5,7,"s", Direction.S)]

    public void Rover_ShouldSetCurrentPosition(int x, int y, string direction, Direction expectedDirection)
    {
        var rover = new Rover(new RoverPosition(x.ToString(), y.ToString(), direction));
        
        Assert.Equal(x, rover.CurrentRoverPosition.XAxis);
        Assert.Equal(y, rover.CurrentRoverPosition.YAxis);
        Assert.Equal(expectedDirection, rover.CurrentRoverPosition.Direction);
        Assert.Equal(1, rover.PreviousPositions.Count);
        Assert.Contains(rover.CurrentRoverPosition, rover.PreviousPositions);
    }
    
    [Theory]
    [InlineData("1","2","p")]
    [InlineData("d","2","e")]
    [InlineData("1","f","n")]
    public void Rover_WrongDirectionShouldThrowException(string x, string y, string direction)
    {
       Assert.Throws<ArgumentException>( () =>  new Rover(new RoverPosition(x, y, direction)));
       
    }

    [Theory]
    [InlineData(1,2,"E", Direction.N)]
    [InlineData(12,23,"n", Direction.W)]
    [InlineData(11,29,"W", Direction.S)]
    [InlineData(5,7,"s", Direction.E)]

    public void SpinLeft_ShouldSetTheCorrectPosition(int x, int y, string direction, Direction expectedDirection)
    {
        var rover = new Rover(new RoverPosition(x.ToString(), y.ToString(), direction));

        rover.SpinLeft();
        
        Assert.Equal(x, rover.CurrentRoverPosition.XAxis);
        Assert.Equal(y, rover.CurrentRoverPosition.YAxis);
        Assert.Equal(expectedDirection, rover.CurrentRoverPosition.Direction);
        Assert.Equal(2, rover.PreviousPositions.Count);
        Assert.Contains(rover.CurrentRoverPosition, rover.PreviousPositions);
    }
    
    [Theory]
    [InlineData(1,2,"E", Direction.S)]
    [InlineData(12,23,"n", Direction.E)]
    [InlineData(11,29,"W", Direction.N)]
    [InlineData(5,7,"s", Direction.W)]

    public void SpinRight_ShouldSetTheCorrectPosition(int x, int y, string direction, Direction expectedDirection)
    {
        var rover = new Rover(new RoverPosition(x.ToString(), y.ToString(), direction));

        rover.SpinRight();
        
        Assert.Equal(x, rover.CurrentRoverPosition.XAxis);
        Assert.Equal(y, rover.CurrentRoverPosition.YAxis);
        Assert.Equal(expectedDirection, rover.CurrentRoverPosition.Direction);
        Assert.Equal(2, rover.PreviousPositions.Count);
        Assert.Contains(rover.CurrentRoverPosition, rover.PreviousPositions);
    }
    
    [Theory]
    [InlineData(1,2,"E", Direction.E)]
    [InlineData(12,23,"n", Direction.N)]
    [InlineData(11,29,"W", Direction.W)]
    [InlineData(5,7,"s", Direction.S)]

    public void MoveForward_ShouldSetNewCoordinates(int x, int y, string direction, Direction expectedDirection)
    {
        var rover = new Rover(new RoverPosition("34", "34", direction));

        rover.MoveForward(x,y);
        
        Assert.Equal(x, rover.CurrentRoverPosition.XAxis);
        Assert.Equal(y, rover.CurrentRoverPosition.YAxis);
        Assert.Equal(expectedDirection, rover.CurrentRoverPosition.Direction);
        Assert.Equal(2, rover.PreviousPositions.Count);
        Assert.Contains(rover.CurrentRoverPosition, rover.PreviousPositions);
    }
}