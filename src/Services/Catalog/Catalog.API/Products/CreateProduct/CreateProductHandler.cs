
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>; // Lenh nay yeu cau 1 ket qua kieu CreateProductResult
    public record CreateProductResult(Guid Id);

    //lớp triển khai IRequestHandler. IRequestHandler<CreateProductCommand, CreateProductResult> Xu ly lenh CreateProductCommand và trả về CreateProductResult
    internal class CreateProductCommandHandler (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        //Phương thức Handle nhận lệnh CreateProductCommand và một CancellationToken.

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
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
    }
}
