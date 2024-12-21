
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>; // Lenh nay yeu cau 1 ket qua kieu CreateProductResult
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }


    //lớp triển khai IRequestHandler. IRequestHandler<CreateProductCommand, CreateProductResult> Xu ly lenh CreateProductCommand và trả về CreateProductResult
    internal class CreateProductCommandHandler 
        (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        //Phương thức Handle nhận lệnh CreateProductCommand và một CancellationToken.
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                
                //create product emtity from command object
                var product = new Product
                {
                    Name = command.Name,
                    Category = command.Category,
                    Description = command.Description,
                    ImageFile = command.ImageFile,
                    Price = command.Price,
                };
                //TODO
                //save database
                session.Store(product);
                await session.SaveChangesAsync(cancellationToken);
      
                //return CreateProductResult result;
                return new CreateProductResult(product.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }
}
