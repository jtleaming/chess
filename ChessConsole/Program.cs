using System;
using ChessEngine;
using ChessEngine.Common;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Chess";
            var chessGame = new Game();
            var enPassantChecker = new EnPassant();
            chessGame.CreateGame(enPassantChecker);
            PlayGame.Play(chessGame);
        }
    }
}
