using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.StoreBasker
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async ([FromBody] StoreBasketRequest request, [FromServices] ISender sender) =>
            {
                    var command = request.Adapt<StoreBasketCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<StoreBasketResponse>();

                    return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("CreateBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Create Basket");
        }
    }
}
