using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jokes.Services.Commands.Requests;
using Jokes.Services.Queries.Requests;

namespace Jokes.Api.Controllers
{
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JokesController(IMediator mediator)
        {
            _mediator = mediator;
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
        [Route("/api/v1/jokes/remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveJokeRequest removeJokeRequest)
        {
            try
            {
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
                var getRequest = new GetJokeRequest
                {
                    Id = id
                };

                return Ok(await _mediator.Send(getRequest));
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
                var getRequest = new GetAllJokesRequests();

                return Ok(await _mediator.Send(getRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
