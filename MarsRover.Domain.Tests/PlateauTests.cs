using MarsRover.Domain.Entities.Plateaus;
using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Tests;

public class PlateauTests
{
    [Theory]
    [InlineData(1,1)]
    [InlineData(5,5)]
    [InlineData(15,5)]
    public void Plateau_ShouldSetExpectedCoordinates(int x, int y)
    {
        var plateau = new Plateau(x, y);
        
        
        Assert.Equal(x, plateau.UpperXAxis); 
        Assert.Equal(y, plateau.UpperYAxis); 

    }
    
    [Theory]
    [InlineData(0,0)]
    [InlineData(0,1)]
    [InlineData(1,0)]
    [InlineData(-10,-10)]
    [InlineData(10,-10)]
    public void Plateau_InvalidValueShouldThrowArgumentException(int x, int y)
    {
        Assert.Throws<ArgumentException>(() => new Plateau(x, y));

    }
    
    [Theory]
    [InlineData(1,1,1,2,"N")]
    [InlineData(1,1,1,0,"s")]
    [InlineData(1,1,2,1,"e")]
    [InlineData(1,1,0,1,"w")]
    [InlineData(0,0,0,0,"s")]
    [InlineData(2,5,2,5,"n")]
    [InlineData(0,1,0,1,"w")]
    [InlineData(5,1,5,1,"e")]
    public void GetNextRoverPosition_ShouldSetExpectedCoordinates(int x, int y, int newX, int newY,string direction)
    {
        var plateau = new Plateau(5, 5);
        var initialPosition = new RoverPosition(x.ToString(), y.ToString(), direction);

        var newPosition = plateau.GetNextRoverPosition(initialPosition);
        
        Assert.Equal(newX, newPosition.XAxis); 
        Assert.Equal(newY, newPosition.YAxis); 

    }
    
    [Theory]
    [InlineData(0,0,"s")]
    [InlineData(2,5,"n")]
    [InlineData(0,1,"w")]
    [InlineData(5,1,"e")]
    public void GetNextRoverPosition_OutOfBoundsSettingWorks(int x, int y, string direction)
    {
        var plateau = new Plateau(5, 5, true);
        var initialPosition = new RoverPosition(x.ToString(), y.ToString(), direction);

       Assert.Throws<PlateauOutOfBoundsException>(() => plateau.GetNextRoverPosition(initialPosition));

    }
}