using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DataStorage.Exceptions
{
    public class LoadGameFileException : Exception
    {
        public LoadGameFileException()
        {
        }

        public LoadGameFileException(string message) : base(message)
        {
        }

        public LoadGameFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
