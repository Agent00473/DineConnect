using DineConnect.OrderManagementService.Application.Features.Customers.Command;
using DineConnect.OrderManagementService.Application.Features.Customers.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DineConnect.OrderManagementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ISender _mediator;

        public CustomerController(ISender mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> Get(int PageNumber = 1, int PageSize = 10)
        {
            var qry = new CustomerQuery(PageNumber, PageSize);
            var result = await _mediator.Send(qry);
            if (result.IsSuccess)
            {
                var responses = result.Value;
                return Ok(responses);
            }
            return NotFound(result.Error);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerResponse>> Get(int id)
        {
            return await Task.FromResult(Ok(new CustomerResponse(Guid.NewGuid(), "Test Customer", "cusrtomer@test.com", 
                new AddressResponse(Guid.NewGuid(), "Street" , "Aalborg", "9000"))));
        }

        // POST api/<CustomerController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CustomerCommandModel> models)
        {

            var cmd = new CreateCustomerCommand(models);
            var result = await _mediator.Send(cmd);
            if (result.IsSuccess)
            {
                var responses = result.Value;
                return Ok(responses);
            }
            return BadRequest(result.Error);
        }

        // POST api/Customer/AddCustomer
        [HttpPost("AddCustomer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerCommandModel customer)
        {
            var lst = new List<CustomerCommandModel> { customer };
            var cmd = new CreateCustomerCommand(lst);
            var result = await _mediator.Send(cmd);
            if (result.IsSuccess)
            {
                var responses = result.Value;
                return Ok(responses);
            }
            return BadRequest(result.Error);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> Put(int id, [FromBody] CustomerCommandModel value)
        {
            return await Task.FromResult(Ok());
        }

        // DELETE api/<CustomerController>/5
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
