using System;
using Jokes.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Jokes.Infra.Interfaces
{
    public interface IJokesRepository
    {
        Task<Joke> Create(Joke joke);
        Task<Joke> Update(Joke joke);
        Task<Joke> Get(string id);
        Task<IList<Joke>> Get();
        Task Remove(string id);
    }
}
