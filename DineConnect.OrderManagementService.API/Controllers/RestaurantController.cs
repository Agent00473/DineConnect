using DineConnect.OrderManagementService.Contracts.Requests;
using DineConnect.OrderManagementService.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace DineConnect.OrderManagementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        // GET: api/<RestaurantController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RestaurantResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RestaurantResponse>>> Get()
        {
            return await Task.FromResult(Ok(new List<RestaurantResponse>()));
        }

        // GET api/<RestaurantController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RestaurantResponse>> Get(int id)
        {
            return await Task.FromResult(Ok(new RestaurantResponse(Guid.NewGuid().ToString(), "TEMPRESTAURANT")));
        }

        // POST api/<RestaurantController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] NewRestaurantRequest value)
        {
            return await Task.FromResult(Created());
        }

        // PUT api/<RestaurantController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] NewRestaurantRequest value)
        {
            return await Task.FromResult(Ok());
        }

        // DELETE api/<RestaurantController>/5
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
