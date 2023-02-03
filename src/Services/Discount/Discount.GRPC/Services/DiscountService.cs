using AutoMapper;
using Discount.Business.repositories;
using Discount.GRPC.Protos;
using Grpc.Core;

namespace Discount.GRPC.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Coupon> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if(coupon == null) 
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} not found"));
            }

            _logger.LogInformation("Discount received for Product Name : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
            return _mapper.Map<Coupon>(coupon);
        }

        public override async Task<Coupon> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Business.Entities.Coupon>(request.Coupon);

            await _repository.CreateDiscount(coupon);
            _logger.LogInformation("Discount is successfully created for Product Name = {productName}", coupon.ProductName);

            return _mapper.Map<Coupon>(coupon);
        }

        public override async Task<Coupon> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Business.Entities.Coupon>(request.Coupon);

            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation("Discount is successfully updated for Product Name = {productName}", coupon.ProductName);

            return _mapper.Map<Coupon>(coupon);
        }

        public override async Task<DeleteDiscountReply> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);
            _logger.LogInformation("Discount is successfully deleted for Product Name = {productName}", request.ProductName);
            
            return new DeleteDiscountReply()
            {
                Success = true
            };
        }
    }
}
