using MediatR;
using Jokes.Services.Commands.Responses;

namespace Jokes.Services.Commands.Requests
{
    public class CreateJokeRequest : IRequest<CreateJokeResponse>
    {
        public string Text { get; set; }
        public string Response { get; set; }

    }
}
