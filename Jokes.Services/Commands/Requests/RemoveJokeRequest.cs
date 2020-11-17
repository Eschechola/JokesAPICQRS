using System;
using MediatR;
using Jokes.Services.Commands.Responses;

namespace Jokes.Services.Commands.Requests
{
    public class RemoveJokeRequest : IRequest<RemoveJokeResponse>
    {
        public string Id { get; set; }
    }
}
