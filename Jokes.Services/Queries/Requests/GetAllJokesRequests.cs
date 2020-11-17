using MediatR;
using Jokes.Services.Queries.Responses;

namespace Jokes.Services.Queries.Requests
{
    public class GetAllJokesRequests : IRequest<GetAllJokesResponse>
    {}
}
