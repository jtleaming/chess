using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
        }

        public override IPlayer Player => throw new System.NotImplementedException();

        public override ISquare Square { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        protected override bool CheckRules(ISquare newSquare)
        {
            throw new System.NotImplementedException();
        }
    }
}