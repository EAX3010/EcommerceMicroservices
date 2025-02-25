namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageUrl,
        decimal Price)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess = false);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            _ = RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .Must(x => x != Guid.Empty).WithMessage("Id cannot be empty GUID");


            _ = RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(1, 32).WithMessage("Name must be between 3 and 32 characters");

            _ = RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .Must(x => x != null && x.Any()).WithMessage("At least one category is required")
                .ForEach(category =>
                {
                    _ = category.NotEmpty().WithMessage("Category item cannot be empty")
                        .Length(2, 50).WithMessage("Each category must be between 2 and 50 characters");
                });

            _ = RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(1, 255).WithMessage("Description must be between 1 and 255 characters");

            _ = RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl is required")
                .Length(1, 255).WithMessage("ImageUrl must be between 1 and 255 characters")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("ImageUrl must be a valid URL");

            _ = RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .LessThan(1000000).WithMessage("Price must be less than 1,000,000");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await session.LoadAsync<Product>(request.Id, cancellationToken) ?? throw new ProductNotFoundException("Products", request.Id);
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageUrl = request.ImageUrl;
            product.Price = request.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}