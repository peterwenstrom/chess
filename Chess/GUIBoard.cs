﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Chess.GameEngine;

namespace Chess
{
    public class GUIBoard
    {
        public ObservableCollection<GUIPiece> Pieces { get; set; }

        public GUIBoard(Board gameBoard)
        {
            Pieces = new ObservableCollection<GUIPiece>();

            foreach (var piece in gameBoard.GameBoard)
            {
                if (piece != null)
                {
                    Pieces.Add(new GUIPiece(ToPoint(piece.Position), piece.Owner.Color,
                        GUIPiece.GetImageSource(piece.Owner.Color, piece.Type)));
                }
            }
        }

        public bool IsApprovedSelection(Point point, PlayerColor color)
        {
            foreach (var piece in Pieces)
            {
                if (piece.XPosition == point.X && piece.YPosition == point.Y && piece.Color == color)
                    return true;
            }
            return false;
        }

        public void UpdatePieces(Coordinates from, Coordinates to)
        {
            Point pointFrom = ToPoint(from);
            Point pointTo = ToPoint(to);
            GUIPiece pieceToBeRemoved = null;
            foreach(var piece in Pieces)
            {
                if (piece.XPosition == pointTo.X && piece.YPosition == pointTo.Y)
                    pieceToBeRemoved = piece;
                else if (piece.XPosition == pointFrom.X && piece.YPosition == pointFrom.Y)
                {
                    piece.XPosition = pointTo.X;
                    piece.YPosition = pointTo.Y;
                }
            }
            if (pieceToBeRemoved != null)
                Pieces.Remove(pieceToBeRemoved);
        }

        public Coordinates ToCoordinates(Point point)
        {
            return new Coordinates((int)(point.Y / 60), (int)(point.X / 60));
        }

        public Point ToPoint(Coordinates coordinates)
        {
            return new Point(coordinates.Column * 60.0 + 1.0, coordinates.Row * 60.0 + 1.0);
        }

        
    }
}
