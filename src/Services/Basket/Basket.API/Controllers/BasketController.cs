using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            if(String.IsNullOrEmpty(userName))
            {
                return BadRequest("Empty Username");
            }
            return Ok((await _repository.GetBasketAsync(userName)) ?? new ShoppingCart(userName));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            if(shoppingCart == null || String.IsNullOrEmpty(shoppingCart.UserName))
            {
                return BadRequest("Shopping is null or empty username");
            }
            return Ok(await _repository.UpdateBasketAsync(shoppingCart));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return BadRequest("Empty Username");
            }
            await _repository.DeleteBasketAsync(userName);
            return Ok();
        }
    }
}
