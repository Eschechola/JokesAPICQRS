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
    public class CreateJokeHandler : IRequestHandler<CreateJokeRequest, CreateJokeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IJokesRepository _jokeRepository;

        public CreateJokeHandler(IMapper mapper, IJokesRepository jokeRepository)
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
    }
}
