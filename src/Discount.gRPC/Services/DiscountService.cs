using Grpc.Core;
using Discount.gRPC;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(ILogger<DiscountService> logger)
        {
            _logger = logger;
        }

        public override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CouponModel
            {
                ProductName = "",
                Amount = 0,
                Description = ""
            });
        }
    }
}