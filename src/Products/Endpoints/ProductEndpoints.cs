using DataEntities;
using Microsoft.EntityFrameworkCore;
using Products.Data;

namespace Products.Endpoints;

public static class ProductEndpoints
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="routes"></param>
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product");

        group.MapGet("/", async (ProductDataContext db) =>
        {
            return await db.Product.ToListAsync();
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, ProductDataContext db) => {
            return await db.Product.FindAsync(id) is Product product ? Results.Ok(product) : Results.NotFound();
        })
            .WithName("GetProductById")
            .Produces<Product>(StatusCodes.Status200OK)
                        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (Product product, ProductDataContext db) =>
        {
            db.Product.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Product/{product.Id}", product);
        });

        group.MapPut("/{id}", async (int id, Product updatedProduct, ProductDataContext db) =>
        {
            var product = await db.Product.FindAsync(id);
            if (product is null) return Results.NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id}", async (int productId, ProductDataContext db) =>
        {
            var product = await db.Product.FindAsync(productId);
            if (product is null) return Results.NotFound();
            db.Product.Remove(product);
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
