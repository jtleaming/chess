using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessEngine;
using ChessEngine.Interfaces;

namespace ChessConsole
{
    public static class PlayGame
    {
        public static void Play(Game chessGame)
        {
            var gameStarted = true;
            string errorMessage = string.Empty;
            while(gameStarted)
            {
                var playerToMove = GetPlayer(chessGame);

                Console.WriteLine(DrawBoard(chessGame.Board.Squares.ToList()));
                Console.WriteLine(errorMessage);
                Console.WriteLine($"Player {playerToMove.IsPlayer}'s move:");
                var moves = Console.ReadLine().Split(' ');

                if(moves[0] == "exit")
                {
                    Console.WriteLine("Game ended");
                    gameStarted = false;
                }
                else
                {
                    try
                    {
                        Console.Clear();
                        playerToMove.Move(moves[0], moves[1]);
                        errorMessage = string.Empty;
                    }
                    catch (Exception e)
                    {
                        errorMessage = e.Message;
                    }
                }
            }
        }

        private static IPlayer GetPlayer(Game chessGame)
        {
            return chessGame.Players.PlayerOne.Turn ? chessGame.Players.PlayerOne : chessGame.Players.PlayerTwo;
        }

        private static string DrawBoard(List<KeyValuePair<string, ISquare>> squares)
        {
            StringBuilder board = new StringBuilder();
            char currentRank = '1';
            int j = 64;
            for (int i = 57; i <= j; i++)
            {
                if (squares[i - 1].Value.Occupied)
                {
                    board.Append($"{squares[i - 1].Value.Piece.GetType().Name.First()}  ");
                }
                else
                {
                    board.Append($"{squares[i - 1].Key} ");
                }
                currentRank = squares[i - 1].Value.Position.rank;
                if (i % 8 == 0 && i != 8)
                {
                    i -= 16;
                    j -= 8;
                    board.AppendLine();
                }
            }
            return board.ToString();
        }
    }
}