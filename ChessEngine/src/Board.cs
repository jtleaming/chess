using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static ChessEngine.Common.Singletons;
using ChessEngine.Extensions;
using ChessEngine.Interfaces;
using ChessEngine.Exceptions;

namespace ChessEngine
{
    public class Board : IBoard
    {
        public Dictionary<string, ISquare> Squares { get; } = new Dictionary<string, ISquare>();
        public Board()
        {
            for (int i = 0; i < 64; i++)
            {
                Squares.Add(File[i].ToString()+Rank[i], new Square(File[i], Rank[i]));
            }
        }

        public void CheckForPiecesBetween(string pieceToMove, string locationToMove)
        {
            List<ISquare> squares = new List<ISquare>();

            if(pieceToMove[0] != locationToMove[0] && pieceToMove[1] != locationToMove[1])
            {
                squares = CheckDiagonalLeaps(pieceToMove, locationToMove).ToList();
            }
            else if(pieceToMove[0] != locationToMove[0] && pieceToMove[1] == locationToMove[1])
            {
                squares = CheckHorizontalLeaps(pieceToMove, locationToMove).ToList();
            }
            else if(pieceToMove[0] == locationToMove[0] && pieceToMove[1] != locationToMove[1])
            {
                squares = CheckVerticalLeaps(pieceToMove, locationToMove).ToList();
            }

            if(squares.Any(s => s.Occupied))
            {
                throw new InvalidMoveException("Piece cannot leap over other piece.");
            }
        }

        private IEnumerable<ISquare> CheckVerticalLeaps(string pieceToMove, string locationToMove)
        {
            int numberOfSquaresToCheck = GetNumberOfSquares(locationToMove[1], pieceToMove[1]);
            if(locationToMove[1] > pieceToMove[1])
            {
                for (int i = 1; i <= numberOfSquaresToCheck; i++)
                {
                    yield return Squares[pieceToMove[0] + ((char)(pieceToMove[1] + i)).ToString()];
                }
            }
            else
            {
                for (int i = 1; i <= numberOfSquaresToCheck; i++)
                {
                    yield return Squares[pieceToMove[0] + ((char)(pieceToMove[1] - i)).ToString()];
                }
            }
        }

        private IEnumerable<ISquare> CheckHorizontalLeaps(string pieceToMove, string locationToMove)
        {
            int numberOfSquaresToCheck = GetNumberOfSquares(pieceToMove[0], locationToMove[0]);
            if(pieceToMove[0] < locationToMove[0])
            {
                for (int i = 1; i <= numberOfSquaresToCheck; i++)
                {
                    yield return Squares[((char)(pieceToMove[0] + i)).ToString() + pieceToMove[1]];
                }
            }
            else
            {
                for (int i = 1; i <= numberOfSquaresToCheck; i++)
                {
                    yield return Squares[((char)(pieceToMove[0] - i)).ToString() + pieceToMove[1]];
                }
            }
        }

        private IEnumerable<ISquare> CheckDiagonalLeaps(string pieceToMove, string locationToMove)
        {
            int numberOfSquaresToCheck = GetNumberOfSquares(pieceToMove[0], locationToMove[0]);
            if(pieceToMove[1] < locationToMove[1])
            {
                for (int i = 1; i <= numberOfSquaresToCheck; i++)
                {
                    yield return Squares[((char)(pieceToMove[0] + i)).ToString() + (char)(pieceToMove[1] + i)];
                }
            }
            else
            {
                for (int i = 1; i <= numberOfSquaresToCheck; i++)
                {
                    yield return Squares[((char)(pieceToMove[0] - i)).ToString() + (char)(pieceToMove[1] - i)];
                }
            }
        }

        private int GetNumberOfSquares(char pieceToMove, char locationToMove)
        {
            if(locationToMove > pieceToMove)
            {
                return Math.Abs(locationToMove - pieceToMove - 1);
            }
            else
            {
                return Math.Abs(locationToMove - pieceToMove + 1);
            }
        }
    }
} 