using Jokes.Infra.Interfaces;
using Jokes.Services.Queries.Requests;
using Jokes.Services.Queries.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jokes.Services.Handlers
{
    public class GetJokeHandler : IRequestHandler<GetJokeRequest, GetJokeResponse>
    {
        private readonly IJokesRepository _jokesRepository;

        public GetJokeHandler(IJokesRepository jokesRepository)
        {
            _jokesRepository = jokesRepository;
        }

        public async Task<GetJokeResponse> Handle(GetJokeRequest request, CancellationToken cancellationToken)
        {
            var joke = await _jokesRepository.Get(request.Id);

            return new GetJokeResponse
            {
                Id = joke.Id,
                Text = joke.Text,
                Response = joke.Response
            };
        }
    }
}
