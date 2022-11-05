namespace MarsRover.Domain.Entities.Rovers;

public record RoverPosition
{
    public RoverPosition(string xAxis, string yAxis, string direction)
    {
        SetXAxis(xAxis);
        SetYAxis(yAxis);
        SetDirection(direction);
    }

    private void SetXAxis(string xAxis)
    {
        if (!int.TryParse(xAxis, out var result))
        {
            throw new ArgumentException($"{xAxis} is not a valid number");

        }

        XAxis = result;
    }
    private void SetYAxis(string yAxis)
    {
        if (!int.TryParse(yAxis, out var result))
        {
            throw new ArgumentException($"{yAxis} is not a valid number");

        }

        YAxis = result;
    }
    private void SetDirection(string direction)
    {
        if (Enum.IsDefined(typeof(Direction), direction.ToUpper()) is false)
        {
            throw new ArgumentException($"{direction} is not a valid direction, use one of N,S,W,E");
        }

        Direction = Enum.Parse<Direction>(direction.ToUpper());
    }

    public int XAxis { get; internal set; }
    public int YAxis { get; internal set; }
    public Direction Direction { get; internal set; }
}