using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Structurals.Requests;

namespace Server.EndPoint.Structurals.Commands
{

    public static class CreateStructuralEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Structurals.EndPoint.Create, async (CreateStructuralRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Structural>() == null || project.BudgetItems.OfType<Structural>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Structural.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Structurals.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Structural Map(this CreateStructuralRequest request, Structural row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
    
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            return row;
        }

    }

}
