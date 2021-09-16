using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jogos.Exceptions
{
    public class NonExistingGameException : Exception
    {
        public NonExistingGameException()
            : base("This game doesn't exist.")
        { }
    }
}