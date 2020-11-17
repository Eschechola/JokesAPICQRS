using MediatR;
using Jokes.Infra.Interfaces;
using Jokes.Services.Queries.Requests;
using Jokes.Services.Queries.Responses;
using System.Threading.Tasks;
using System.Threading;

namespace Jokes.Services.Handlers
{
    public class GetAllJokesHandler : IRequestHandler<GetAllJokesRequests, GetAllJokesResponse>
    {
        private readonly IJokesRepository _jokesRepository;

        public GetAllJokesHandler(IJokesRepository jokesRepository)
        {
            _jokesRepository = jokesRepository;
        }

        public async Task<GetAllJokesResponse> Handle(GetAllJokesRequests request, CancellationToken cancellationToken)
        {
            var allJokes = await _jokesRepository.Get();

            return new GetAllJokesResponse
            {
                Jokes = allJokes
            };
        }
    }
}
