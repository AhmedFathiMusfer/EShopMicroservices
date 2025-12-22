
using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace Discount.Grpc.Services
{
    // ...existing code...
    public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var copoun = request.Adapt<Coupon>();
            dbContext.Coupons.Add(copoun);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Discount is successfully created for ProductName : {request.CouponModel.ProductName}");

            return new CouponModel
            {
                ProductName = request.CouponModel.ProductName,
                Description = request.CouponModel.Description,
                Amount = request.CouponModel.Amount
            };
        }
        override public async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var copoun = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (copoun == null)
            {
                return new CouponModel
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount Desc"
                };
            }
            logger.LogInformation($"Discount is retrieved for ProductName : {copoun.ProductName} , Amount : {copoun.Amount}");
            return new CouponModel
            {
                Id = copoun.Id,
                ProductName = copoun.ProductName,
                Description = copoun.Description,
                Amount = copoun.Amount
            };

        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var copoun = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.CouponModel.Id);
            if (copoun == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id={request.CouponModel.Id} is not found"));
            }
            logger.LogInformation($"Discount is Updated for ProductName : {copoun.ProductName} , Amount : {copoun.Amount}");
            copoun.ProductName = request.CouponModel.ProductName;
            copoun.Description = request.CouponModel.Description;
            copoun.Amount = request.CouponModel.Amount;
            await dbContext.SaveChangesAsync();
            return new CouponModel
            {
                Id = copoun.Id,
                ProductName = copoun.ProductName,
                Description = copoun.Description,
                Amount = copoun.Amount
            };
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var copoun = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if (copoun == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found"));
            }
            logger.LogInformation($"Discount is Deleted for ProductName : {copoun.ProductName} , Amount : {copoun.Amount}");
            dbContext.Coupons.Remove(copoun);
            await dbContext.SaveChangesAsync();
            var response = new DeleteDiscountResponse
            {

            };
            return response;
        }
    }
}