using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Foundations.Requests;

namespace Server.EndPoint.Foundations.Commands
{

    public static class CreateFoundationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Foundations.EndPoint.Create, async (CreateFoundationRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Foundation>() == null || project.BudgetItems.OfType<Foundation>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Foundation.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Foundations.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Foundation Map(this CreateFoundationRequest request, Foundation row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
        
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            return row;
        }

    }

}
