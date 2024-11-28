using Grpc.Core;
using Discount.gRPC;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        // Assuming a discount repository or service is used to fetch data
        private readonly IDiscountRepository _discountRepository; 

        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository discountRepository)
        {
            _logger = logger;
            _discountRepository = discountRepository;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            try
            {
                // Validate request
                if (string.IsNullOrEmpty(request.ProductName))
                {
                    _logger.LogWarning("Invalid request received");
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Product name must be specified."));
                }

                // Fetch discount information
                var discount = await _discountRepository.GetDiscountAsync(request.ProductName);
                if (discount == null)
                {
                    _logger.LogWarning($"No discount found for product {request.ProductName}");
                    throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found for product {request.ProductName}."));
                }

                // Log the successful fetch
                _logger.LogInformation($"Discount retrieved for product {request.ProductName}");

                // Return the fetched discount
                return new CouponModel
                {
                    ProductName = discount.ProductName,
                    Amount = discount.Amount,
                    Description = discount.Description
                };
            }
            catch (RpcException)
            {
                throw;  // Re-throw known RPC exceptions
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the discount.");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request."));
            }
        }
    }
}