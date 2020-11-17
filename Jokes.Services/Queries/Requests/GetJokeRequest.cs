using System;
using MediatR;
using Jokes.Services.Queries.Responses;

namespace Jokes.Services.Queries.Requests
{
    public class GetJokeRequest : IRequest<GetJokeResponse>
    {
        public string Id { get; set; }
    }
}
