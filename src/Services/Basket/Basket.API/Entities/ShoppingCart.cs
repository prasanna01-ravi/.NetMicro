namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string? UserName { get; set; }
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

        public ShoppingCart()
        {
        }

        public ShoppingCart(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        public decimal TotalPrice
        {
            get
            {
                return this.ShoppingCartItems?.Sum(si => si.Quantity * si.Price) ?? 0;
            }
        }
    }
}
