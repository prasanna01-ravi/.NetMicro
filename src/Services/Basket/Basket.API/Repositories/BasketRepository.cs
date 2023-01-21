using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart?> GetBasketAsync(string username)
        {
            if(_redisCache == null || String.IsNullOrEmpty(username))
            {
                return null;
            }
            string shoppingCartVal = await _redisCache.GetStringAsync(username) ?? "";
            if (String.IsNullOrEmpty(shoppingCartVal))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(shoppingCartVal);
        }

        public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart shoppingCart)
        {
            if(shoppingCart == null || String.IsNullOrEmpty(shoppingCart.UserName))
            {
                return null;
            }
            await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasketAsync(shoppingCart.UserName);
        }
    }
}
