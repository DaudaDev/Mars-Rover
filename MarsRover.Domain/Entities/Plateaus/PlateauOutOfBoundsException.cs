namespace MarsRover.Domain.Entities.Plateaus;

public class PlateauOutOfBoundsException : Exception
{
    public PlateauOutOfBoundsException(string message) : base(message)
    {
        
    }
}