using MarsRover.Domain.Entities.Rovers;

namespace MarsRover.Domain.Entities.Parsers;

public class Parser
{
    // With more time i will implement proper file validation and sanitation here.
    // This is for security reasons and failing fast
    // I will also validate the inputs here.
    public IReadOnlyCollection<NavigatorCommand> GetNavigationCommands(Stream stream)
    {
        List<NavigatorCommand> navigatorCommands = new();

        try
        { 
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var command = GetNavigatorCommand(reader);
                    navigatorCommands.Add(command);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Error parsing input, {e.Message}");
        }
       

        return navigatorCommands;
    }

    private static NavigatorCommand GetNavigatorCommand(StreamReader reader)
    {
        var line = reader.ReadLine();
        if (line is null)
        {
            throw new ArgumentException("File has no content");
        }
        
        var segments = line.Split('|');
        var positionSegment = segments[0].Split(" ");
        var movements = segments[1]
            .Select(movement => Enum.Parse<Movement>(movement.ToString()))
            .ToList();

        var command = new NavigatorCommand(positionSegment[0], positionSegment[1], positionSegment[2], movements);
        return command;
    }
}