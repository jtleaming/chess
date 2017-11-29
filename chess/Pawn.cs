using System.Linq;

public class Pawn
{
    public char[] StartingPosition;
    public char[] CurrentPosition;


    public Pawn(string StartingPosition)
    {
        this.StartingPosition = StartingPosition.ToCharArray();
    }
}