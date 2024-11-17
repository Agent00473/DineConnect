using DineConnect.OrderManagementService.API.Common;
using DineConnect.OrderManagementService.Application.Features.Orders.Command;
using DineConnect.OrderManagementService.Application.Features.Orders.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DineConnect.OrderManagementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseAPIController
    {
        private readonly ISender _mediator;
        public OrderController(ISender mediator)
        {
            _mediator = mediator;

        }
        // GET: api/<OrderController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> Get(int PageNumber = 1, int PageSize = 10)
        {
            var qry = new OrderQuery(PageNumber, PageSize);
            var result = await _mediator.Send(qry);
            if (result.IsSuccess)
            {
                var responses = result.Value;
                return Ok(responses);
            }
            return NotFound(result.Error);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> Get(int id)
        {
            return await Task.FromResult(Ok(new OrderResponse()));
        }

        // POST api/<OrderController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] OrderCommandModel value)
        {
            var cmd = new CreateOrderCommand(value);
            var result = await _mediator.Send(cmd);
            if (result.IsSuccess)
            {
                var responses = result.Value;
                return Ok(responses);
            }
            return BadRequest(result.Error);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] OrderCommandModel value)
        {
            return await Task.FromResult(Ok());
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            return await Task.FromResult(Ok());
        }
    }
}
