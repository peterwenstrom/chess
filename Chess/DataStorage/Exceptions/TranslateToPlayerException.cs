using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DataStorage.Exceptions
{
    public class TranslateToPlayerException : Exception
    {
        public TranslateToPlayerException()
        {
        }

        public TranslateToPlayerException(string message) : base(message)
        {
        }

        public TranslateToPlayerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
