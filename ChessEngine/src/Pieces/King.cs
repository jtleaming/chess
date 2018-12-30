using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class King : Piece
    {
        private readonly IPlayer player;

        public override IPlayer Player => player;
        public override ISquare Square { get => base.currentSquare; }
        public King(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
            this.currentSquare = currentSquare;
            this.player = player;
        }

        protected override bool CheckRules(ISquare newSquare)
        {
            throw new System.NotImplementedException();
        }
    }
}