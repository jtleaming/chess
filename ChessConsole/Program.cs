using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine;
using ChessEngine.Interfaces;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var chessGame = new Game();
            chessGame.CreateGame();
            var chessBoard = chessGame.Board;
            var squares = chessBoard.Squares.ToList();
            PlayGame.Play(chessGame);
        }
    }
}
