using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MarsRover.Domain.Entities;
using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Tests;

public class NavigatorTests
{
    [Fact]
    public void GetNavigations_MultipleCommands_ReturnsMultipleRovers()
    {
        var cslLines = new[]
        {
            "1 2 N|LMLMLMLMM",
            "3 3 E|MMRMMRMRRM",
            "2 3 E|MMRMMRMRRM"
        };
        
        var stream = GenerateCsv(cslLines);
        var navigator = new Navigator();

        var result = navigator.GetNavigations(stream);
        
        Assert.Equal(3, result.Rovers.Count);
    }
    
    //With more time i will add more edge cases here
    [Theory]
    [InlineData("1 2 N|LMLMLMLMM", 1, 3, Direction.N)]
    [InlineData("3 3 E|MMRMMRMRRM", 5, 1, Direction.E)]
    public void GetNavigations_ReturnsCorrectFinalPosition(string movement, int finalX, int finalY, Direction direction)
    {
        var cslLines = new[]
        {
           movement
        };
        
        var stream = GenerateCsv(cslLines);
        var navigator = new Navigator();

        var result = navigator.GetNavigations(stream);

        var rover = result.Rovers.ElementAt(0);
        
        Assert.Equal(finalX, rover.previousMovements.Last().XAxis);
        Assert.Equal(finalY, rover.previousMovements.Last().YAxis);
        Assert.Equal(direction, rover.previousMovements.Last().Direction);

    }

    
    [Theory]
    [InlineData("1 2 NLMLMLMLMM")]
    [InlineData("1 2 |NLMLMLMLMM")]
    [InlineData("1 2 N|LMLMLMLHM")]
    [InlineData("1 2 H|LMLMLMLM")]
    [InlineData("1")]
    public void GetNavigations_InvalidInputs_ThrowsException(string csvLine)
    {
        var cslLines = new[]
        {
            "1 2 N|LMLMLMLMM",
            csvLine
        };
        
        var stream = GenerateCsv(cslLines);
        var navigator = new Navigator();
       
        var ex =Record.Exception(() => navigator.GetNavigations(stream));
        
        Assert.NotNull(ex);
    }

    private Stream GenerateCsv(IEnumerable<string> csvLines)
    {
        return 
            new MemoryStream(Encoding.UTF8.GetBytes(string.Join("\n", csvLines)));
    }
}