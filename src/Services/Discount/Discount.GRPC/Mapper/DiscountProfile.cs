using AutoMapper;
using Discount.Business.Entities;

namespace Discount.GRPC.Mapper
{
    public class DiscountProfile : Profile
    {
        protected DiscountProfile()
        {
            CreateMap<Coupon, Protos.Coupon>().ReverseMap();
        }
    }
}
