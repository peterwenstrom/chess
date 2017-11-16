using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace Chess.GUI
{
    public class GUITile : INotifyPropertyChanged
    {
        private double xPosition;
        private double yPosition;

        public GUITile(Point point)
        {
            XPosition = point.X;
            YPosition = point.Y;
        }

        public bool IsSameTile(Point point)
        {
            return (XPosition == point.X && YPosition == point.Y);
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
