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
            while (gameStarted)
            {
                var playerToMove = GetPlayer(chessGame);
                Console.WriteLine(DrawBoard(chessGame.Board.Squares.ToList()));
                Console.WriteLine(errorMessage);
                Console.WriteLine($"Player {playerToMove.IsPlayer}'s move:");
                var moves = Console.ReadLine().Split(' ');

                if (moves[0] == "exit")
                {
                    Console.WriteLine("Game ended");
                    gameStarted = false;
                }
                else
                {
                    try
                    {
                        ClearConsole();
                        playerToMove.Move(moves[0], moves[1]);
                        Console.WriteLine(chessGame.TurnMessage + "\r\n");
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
            board.Append("\t\t\t");
            for (int i = 57; i >= 1; i++)
            {
                if (squares[i - 1].Value.Occupied)
                {
                    var name = squares[i - 1].Value.Piece.GetType().Name;
                    name = name == "Knight" ? "N" : name.First().ToString();
                    board.Append($"{name}  ");
                }
                else
                {
                    board.Append($"{squares[i - 1].Key} ");
                }
                currentRank = squares[i - 1].Value.Position.rank;
                if (i % 8 == 0)
                {
                    i -= 16;
                    board.AppendLine();
                    board.Append("\t\t\t");
                }
            }
            return board.ToString();
        }
        private static void ClearConsole()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
                //Console based integration tests can't clear console
                //since there is technically no buffer to clear.
                //Catch exception and do nothing with it here.
            }
        }
    }
}