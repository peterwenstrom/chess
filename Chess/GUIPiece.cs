using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Chess.GameEngine;

namespace Chess
{
    

    public class GUIPiece : INotifyPropertyChanged
    {
        public string ImageSource { get; set; }
        public PlayerColor Color { get; set; }

        private double xPosition;
        private double yPosition;

        public GUIPiece(Point point, PlayerColor color, string imageSource)
        {
            ImageSource = imageSource;
            Color = color;
            XPosition = point.X;
            YPosition = point.Y;
        }

        public static string GetImageSource(PlayerColor color, PieceType type)
        {
            string directory = "Images/";
            switch (type)
            {
                case PieceType.Bishop:
                    if (color == PlayerColor.White)
                        return directory + "white_bishop.png";
                    else
                        return directory + "black_bishop.png";
                case PieceType.Rook:
                    if (color == PlayerColor.White)
                        return directory + "white_rook.png";
                    else
                        return directory + "black_rook.png";
                case PieceType.Queen:
                    if (color == PlayerColor.White)
                        return directory + "white_queen.png";
                    else
                        return directory + "black_queen.png";
                case PieceType.Pawn:
                    if (color == PlayerColor.White)
                        return directory + "white_pawn.png";
                    else
                        return directory + "black_pawn.png";
                case PieceType.King:
                    if (color == PlayerColor.White)
                        return directory + "white_king.png";
                    else
                        return directory + "black_king.png";
                case PieceType.Knight:
                    if (color == PlayerColor.White)
                        return directory + "white_knight.png";
                    else
                        return directory + "black_knight.png";
                default:
                    return null;
            }
                

        }

        public double XPosition
        {
            get
            {
                return this.xPosition;
            }
            set
            {
                if (value != this.xPosition)
                {
                    this.xPosition = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double YPosition
        {
            get
            {
                return this.yPosition;
            }
            set
            {
                if (value != this.yPosition)
                {
                    this.yPosition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
