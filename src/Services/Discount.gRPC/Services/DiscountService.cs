namespace Discount.gRPC.Services
{
    public class DiscountService(MyDBContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.ProductName))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "ProductName is required"));
            logger.LogInformation("Fetching discount for product: {ProductName}", request.ProductName);
            var productName = request.ProductName;
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(c => c.ProductName == productName);
            if (coupon == null)
            {
                logger.LogWarning("No coupon found for product: {ProductName}", productName);
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }

            logger.LogInformation("Discount found for product: {ProductName}", coupon.ProductName);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request.Coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is required"));

            if (request.Coupon.Amount <= 0 || string.IsNullOrEmpty(request.Coupon.Description) ||
                string.IsNullOrEmpty(request.Coupon.ProductName))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Arguments"));

            logger.LogInformation("Creating discount for product: {ProductName}", request.Coupon.ProductName);

            var Coupon = request.Coupon.Adapt<Coupon>();
            var isExists = await dbContext.Coupons
                .AnyAsync(c => c.ProductName == Coupon.ProductName);

            if (isExists)
            {
                logger.LogWarning("Product: {ProductName} already exist", Coupon.ProductName);
                throw new RpcException(new Status(StatusCode.AlreadyExists, "Coupon is Already Exists"));
            }

            dbContext.Coupons.Add(Coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Created discount for product: {ProductName} with Id: {Id}", Coupon.ProductName,
                Coupon.Id);
            return Coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            if (request.Coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is required"));

            if (request.Coupon.Amount <= 0 || string.IsNullOrEmpty(request.Coupon.Description) ||
                string.IsNullOrEmpty(request.Coupon.ProductName))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Arguments"));

            logger.LogInformation("Updating discount for product: {ProductName}", request.Coupon.ProductName);

            var coupon = request.Coupon.Adapt<Coupon>();
            var count = await dbContext.Coupons
                .Where(c => c.ProductName == coupon.ProductName)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(i => i.Description, coupon.Description)
                    .SetProperty(i => i.Amount, coupon.Amount));

            if (count == 0)
            {
                logger.LogWarning("No coupon found for product: {ProductName}", coupon.ProductName);
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }

            var updatedCoupon = await dbContext.Coupons
                .FirstOrDefaultAsync(c => c.ProductName == coupon.ProductName);

            logger.LogInformation("Updated discount for product: {ProductName}", updatedCoupon?.ProductName);
            return updatedCoupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteResponse> DeleteDiscount(DeleteDiscountRequest request,
            ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.ProductName))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "ProductName is required"));

            logger.LogInformation("Deleting discount for product: {ProductName}", request.ProductName);

            var productName = request.ProductName;
            var count = await dbContext.Coupons
                .Where(c => c.ProductName == productName)
                .ExecuteDeleteAsync();

            if (count == 0)
            {
                logger.LogWarning("No coupon found for product: {ProductName}", productName);
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
            }

            logger.LogInformation("Deleted discount for product: {ProductName}", productName);
            return new DeleteResponse { IsSuccess = true };
        }
    }
}