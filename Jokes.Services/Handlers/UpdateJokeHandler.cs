using MediatR;
using AutoMapper;
using Jokes.Infra.Interfaces;
using Jokes.Services.Commands.Requests;
using Jokes.Services.Commands.Responses;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Jokes.Domain.Entities;
using Jokes.Domain.Validators;

namespace Jokes.Services.Handlers
{
    public class UpdateJokeHandler : IRequestHandler<UpdateJokeRequest, UpdateJokeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJokesRepository _jokeRepository;

        public UpdateJokeHandler(IMapper mapper, IJokesRepository jokeRepository)
        {
            _mapper = mapper;
            _jokeRepository = jokeRepository;
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
    }
}
