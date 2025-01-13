using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.EHSs.Requests;

namespace Server.EndPoint.EHSs.Commands
{

    public static class CreateEHSEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EHSs.EndPoint.Create, async (CreateEHSRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<EHS>() == null || project.BudgetItems.OfType<EHS>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = EHS.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.EHSs.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static EHS Map(this CreateEHSRequest request, EHS row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
         
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            return row;
        }

    }

}
