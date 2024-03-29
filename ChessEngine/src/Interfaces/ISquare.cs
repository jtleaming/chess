using System.Text.Json.Serialization;

namespace ChessEngine.Interfaces
{
    public interface ISquare
    {
        (char file, char rank) Position { get; }
        string Id {get;}
        [JsonIgnore]
        IPiece Piece { get; set; }
        bool Occupied { get; }
    }
}