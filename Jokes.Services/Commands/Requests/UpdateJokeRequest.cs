using Jokes.Services.Commands.Responses;
using MediatR;
using System;

namespace Jokes.Services.Commands.Requests
{
    public class UpdateJokeRequest : IRequest<UpdateJokeResponse>
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Response { get; set; }

    }
}
