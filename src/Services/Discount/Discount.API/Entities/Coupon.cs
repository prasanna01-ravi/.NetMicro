namespace Discount.API.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public String? ProductName { get; set; }
        public String? Description { get; set; }
        public double Amount { get; set; }
    }
}
