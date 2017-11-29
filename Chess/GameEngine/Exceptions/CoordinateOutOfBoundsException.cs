using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameEngine.Exceptions
{
    public class CoordinateOutOfBoundsException : Exception
    {
        public CoordinateOutOfBoundsException()
        {
        }

        public CoordinateOutOfBoundsException(string message) : base(message)
        {
        }

        public CoordinateOutOfBoundsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
