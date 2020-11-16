using MediatR;
using Jokes.Services.Commands.Responses;

namespace Jokes.Services.Commands.Requests
{
    public class CreateJokeRequest : IRequest<CreateJokeResponse>
    {
    }
}
