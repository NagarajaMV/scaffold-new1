using Microsoft.EntityFrameworkCore;
using scaffold_new1.DBCOntext;
namespace scaffold_new1.Models;

public static class uDetailEndpoints
{
    public static void MapPsDetailEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/PsDetail", async (ProductDataContext db) =>
        {
            return await db.PsDetails.ToListAsync();
        })
        .WithName("GetAllPsDetails")
        .Produces<List<PsDetail>>(StatusCodes.Status200OK);

        routes.MapGet("/api/PsDetail/{id}", async (int Id, ProductDataContext db) =>
        {
            return await db.PsDetails.FindAsync(Id)
                is PsDetail model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetPsDetailById")
        .Produces<PsDetail>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/PsDetail/{id}", async (int Id, PsDetail psDetail, ProductDataContext db) =>
        {
            var foundModel = await db.PsDetails.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(psDetail);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatePsDetail")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/PsDetail/", async (PsDetail psDetail, ProductDataContext db) =>
        {
            db.PsDetails.Add(psDetail);
            await db.SaveChangesAsync();
            return Results.Created($"/PsDetails/{psDetail.Id}", psDetail);
        })
        .WithName("CreatePsDetail")
        .Produces<PsDetail>(StatusCodes.Status201Created);

        routes.MapDelete("/api/PsDetail/{id}", async (int Id, ProductDataContext db) =>
        {
            if (await db.PsDetails.FindAsync(Id) is PsDetail psDetail)
            {
                db.PsDetails.Remove(psDetail);
                await db.SaveChangesAsync();
                return Results.Ok(psDetail);
            }

            return Results.NotFound();
        })
        .WithName("DeletePsDetail")
        .Produces<PsDetail>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
