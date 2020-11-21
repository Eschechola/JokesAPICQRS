using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jokes.Services.Commands.Requests;
using Jokes.Services.Queries.Interfaces;

namespace Jokes.Api.Controllers
{
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJokeQueries _jokeQueries;

        public JokesController(IMediator mediator, IJokeQueries jokeQueries)
        {
            _mediator = mediator;
            _jokeQueries = jokeQueries;
        }

        [HttpPost]
        [Route("/api/v1/jokes/create")]
        public async Task<IActionResult> Create([FromBody]CreateJokeRequest createJokeRequest)
        {
            try
            {
                return Ok(await _mediator.Send(createJokeRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("/api/v1/jokes/update")]
        public async Task<IActionResult> Update([FromBody] UpdateJokeRequest updateJokeRequest)
        {
            try
            {
                return Ok(await _mediator.Send(updateJokeRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("/api/v1/jokes/remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                var removeJokeRequest = new RemoveJokeRequest
                {
                    Id = id
                };

                return Ok(await _mediator.Send(removeJokeRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/v1/jokes/get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(await _jokeQueries.Get(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/v1/jokes/get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _jokeQueries.Get());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
