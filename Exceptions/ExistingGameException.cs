using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jogos.Exceptions
{
    public class ExistingGameException : Exception
    {
        public ExistingGameException()
            : base("There is already a game with this name for this publisher.")
        { }
    }
}