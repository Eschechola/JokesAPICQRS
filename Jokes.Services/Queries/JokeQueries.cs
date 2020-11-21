using AutoMapper;
using System.Threading.Tasks;
using Jokes.Infra.Interfaces;
using System.Collections.Generic;
using Jokes.Services.Queries.DTO;
using Jokes.Services.Queries.Interfaces;

namespace Jokes.Services.Queries
{
    public class JokeQueries : IJokeQueries
    {
        private readonly IMapper _mapper;
        private readonly IJokesRepository _jokesRepository;

        public JokeQueries(IMapper mapper, IJokesRepository jokesRepository)
        {
            _mapper = mapper;
            _jokesRepository = jokesRepository;
        }

        public async Task<JokeDTO> Get(string id)
        {
            var joke = await _jokesRepository.Get(id);

            return _mapper.Map<JokeDTO>(joke);
        }

        public async Task<IList<JokeDTO>> Get()
        {
            var allJokes = await _jokesRepository.Get();

            return _mapper.Map<List<JokeDTO>>(allJokes);
        }
    }
}
