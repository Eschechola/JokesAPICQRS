using System;
using MediatR;
using System.Threading;
using Jokes.Infra.Interfaces;
using System.Threading.Tasks;
using Jokes.Services.Commands.Requests;
using Jokes.Services.Commands.Responses;

namespace Jokes.Services.Handlers
{
    public class RemoveJokeHandler : IRequestHandler<RemoveJokeRequest, RemoveJokeResponse>
    {
        private readonly IJokesRepository _jokeRepository;

        public RemoveJokeHandler(IJokesRepository jokeRepository)
        {
            _jokeRepository = jokeRepository;
        }

        public async Task<RemoveJokeResponse> Handle(RemoveJokeRequest request, CancellationToken cancellationToken)
        {
            var jokeExists = await _jokeRepository.Get(request.Id);

            if (jokeExists == null)
                throw new Exception("Piada não encontrada na base de dados.");

            await _jokeRepository.Remove(request.Id);

            return new RemoveJokeResponse
            {
                Message = "Piada deletada com sucesso!"
            };
        }
    }
}
