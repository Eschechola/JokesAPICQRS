using MediatR;
using Jokes.Services.Commands.Requests;
using Jokes.Services.Commands.Responses;
using System.Threading.Tasks;
using System.Threading;

namespace Jokes.Services.Handlers
{
    public class CreateJokeHandler : IRequestHandler<CreateJokeRequest, CreateJokeResponse>
    {
        public async Task<CreateJokeResponse> Handle(CreateJokeRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
