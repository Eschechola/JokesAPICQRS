using MediatR;
using System.Threading;
using Jokes.Infra.Interfaces;
using System.Threading.Tasks;
using Jokes.Services.Commands.Requests;
using Jokes.Services.Commands.Responses;
using AutoMapper;
using Jokes.Domain.Entities;
using Jokes.Domain.Validators;
using System;
using System.Linq;

namespace Jokes.Services.Handlers
{
    public class JokeHandler : IRequestHandler<CreateJokeRequest, CreateJokeResponse>,
                                     IRequestHandler<UpdateJokeRequest, UpdateJokeResponse>,
                                     IRequestHandler<RemoveJokeRequest, RemoveJokeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJokesRepository _jokeRepository;

        public JokeHandler(IMapper mapper, IJokesRepository jokeRepository)
        {
            _mapper = mapper;
            _jokeRepository = jokeRepository;
        }

        public async Task<CreateJokeResponse> Handle(CreateJokeRequest request, CancellationToken cancellationToken)
        {
            var joke = _mapper.Map<Joke>(request);

            var validator = new JokeValidator();
            validator.Validate(joke);

            if(validator.Errors.Count > 0)
                throw new Exception(validator.Errors.FirstOrDefault());

            var jokeCreated = await _jokeRepository.Create(joke);

            return new CreateJokeResponse
            {
                Id = jokeCreated.Id,
                Text = jokeCreated.Text,
                Response = jokeCreated.Response
            };
        }

        public async Task<UpdateJokeResponse> Handle(UpdateJokeRequest request, CancellationToken cancellationToken)
        {
            var joke = _mapper.Map<Joke>(request);

            var validator = new JokeValidator();
            validator.Validate(joke);

            if (validator.Errors.Count > 0)
                throw new Exception(validator.Errors.FirstOrDefault());

            var jokeExists = await _jokeRepository.Get(joke.Id);

            if (jokeExists == null)
                throw new Exception("A piada informada não existe.");

            var jokeCreated = await _jokeRepository.Update(joke);

            return new UpdateJokeResponse
            {
                Id = jokeCreated.Id,
                Text = jokeCreated.Text,
                Response = jokeCreated.Response
            };
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
