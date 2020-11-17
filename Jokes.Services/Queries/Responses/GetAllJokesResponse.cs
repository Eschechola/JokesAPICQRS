using Jokes.Domain.Entities;
using System.Collections.Generic;

namespace Jokes.Services.Queries.Responses
{
    public class GetAllJokesResponse
    {
        public IList<Joke> Jokes  { get; set; }
    }
}
