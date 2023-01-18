namespace TicTacToe.Models;

public class Coordinate : IEquatable<Coordinate>
{
    public int X { get; }
    public int Y { get; }
    public bool Forfeit { get; }

    public Coordinate(bool forfeit)
    {
        Forfeit = forfeit;
    }
    
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Coordinate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y && Forfeit == other.Forfeit;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Coordinate) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Forfeit);
    }
}
