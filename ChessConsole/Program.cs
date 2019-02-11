using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine;
using ChessEngine.Common;
using ChessEngine.Interfaces;

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
