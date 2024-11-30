namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly MyDBContext _dbContext;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(MyDBContext dbContext, ILogger<DiscountService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Fetching discount for product: {ProductName}", request.ProductName);

            var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);

            if (coupon == null)
            {
                _logger.LogWarning("No discount found for product: {ProductName}", request.ProductName);
                return new CouponModel
                {
                    Id = 0,
                    ProductName = "No Discount",
                    Description = "No Discount",
                    Amount = 0,
                };
            }

            _logger.LogInformation("Discount found for product: {ProductName}", request.ProductName);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Creating discount for product: {ProductName}", request.Coupon.ProductName);
            bool isExist = await _dbContext.Coupons
                .AnyAsync(c => c.ProductName == request.Coupon.ProductName);

            if(isExist)
            {
                _logger.LogWarning("Product: {ProductName} already exist", request.Coupon.ProductName);
                return new CouponModel
                {
                    Id = 0,
                    ProductName = request.Coupon.ProductName,
                    Description = "Already exist",
                    Amount = 0,
                };
            }

            var coupon = request.Coupon.Adapt<Coupon>();
            _dbContext.Coupons.Add(coupon);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Created discount for product: {ProductName} with Id: {Id}", coupon.ProductName, coupon.Id);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var count = await _dbContext.Coupons
             .Where(c => c.ProductName == request.Coupon.ProductName)
                  .ExecuteUpdateAsync(c => c
                  .SetProperty(c => c.Description, request.Coupon.Description)
                  .SetProperty(c => c.Amount, request.Coupon.Amount));

            if (count == 0)
            {
                _logger.LogWarning("No coupon found for product: {ProductName}", request.Coupon.ProductName);
                return new CouponModel
                {
                    Id = 0,
                    ProductName = "No Discount",
                    Description = "No Discount",
                    Amount = 0
                };
            }

            var updatedCoupon = await _dbContext.Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.Coupon.ProductName);
            _logger.LogInformation("Updated coupon: {@Coupon}", updatedCoupon);
            return updatedCoupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var count = await _dbContext.Coupons
                .Where(c => c.ProductName == request.ProductName)
                .ExecuteDeleteAsync();

            if (count == 0)
            {
                _logger.LogWarning("No coupon found for product: {ProductName}", request.ProductName);
                return new DeleteResponse { IsSuccess = false };
            }

            _logger.LogInformation("Deleted discount for product: {ProductName}", request.ProductName);
            return new DeleteResponse { IsSuccess = true };
        }
    }
}