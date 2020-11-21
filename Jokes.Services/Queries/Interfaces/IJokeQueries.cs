using System.Threading.Tasks;
using Jokes.Services.Queries.DTO;
using System.Collections.Generic;

namespace Jokes.Services.Queries.Interfaces
{
    public interface IJokeQueries
    {
        Task<JokeDTO> Get(string id);
        Task<IList<JokeDTO>> Get();
    }
}
