namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageUrl,
        decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            _ = RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(1, 32).WithMessage("Name must be between 3 and 32 characters");

            _ = RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .Must(x => x != null && x.Count != 0).WithMessage("At least one category is required")
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

    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Price = request.Price
            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}