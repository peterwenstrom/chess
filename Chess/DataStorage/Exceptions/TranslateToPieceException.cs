using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DataStorage.Exceptions
{
    public class TranslateToPieceException : Exception
    {
        public TranslateToPieceException()
        {
        }

        public TranslateToPieceException(string message) : base(message)
        {
        }

        public TranslateToPieceException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
