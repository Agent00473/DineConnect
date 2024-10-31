using DineConnect.OrderManagementService.Contracts.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DineConnect.OrderManagementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // GET: api/<CustomerController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> Get()
        {
            return await Task.FromResult(Ok(new List<CustomerResponse>()));
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerResponse>> Get(int id)
        {
            return await Task.FromResult(Ok(new CustomerResponse(Guid.NewGuid(), "Test Customer", "cusrtomer@test.com", 
                new DeliveryAddressResponse(Guid.NewGuid(), "Street" , "Aalborg", "9000"))));
        }

        // POST api/<CustomerController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] NewCustomerRequest value)
        {
            return await Task.FromResult(Created());
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> Put(int id, [FromBody] NewCustomerRequest value)
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
