using Jokes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokes.Domain.Validators
{
    public class JokeValidator
    {
        private readonly List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors;
        
        public JokeValidator()
        {
            _errors = new List<string>();
        }

        public bool Validate(Joke joke)
        {
            return true;
        }
    }
}
