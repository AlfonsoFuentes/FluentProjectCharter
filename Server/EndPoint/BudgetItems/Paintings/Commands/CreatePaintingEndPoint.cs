using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Paintings.Requests;

namespace Server.EndPoint.Paintings.Commands
{

    public static class CreatePaintingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Paintings.EndPoint.Create, async (CreatePaintingRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Painting>() == null || project.BudgetItems.OfType<Painting>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Painting.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Paintings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Painting Map(this CreatePaintingRequest request, Painting row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
         
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            return row;
        }

    }

}
