using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Electricals.Requests;

namespace Server.EndPoint.Electricals.Commands
{

    public static class CreateElectricalEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Electricals.EndPoint.Create, async (CreateElectricalRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Electrical>() == null || project.BudgetItems.OfType<Electrical>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Electrical.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Electricals.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Electrical Map(this CreateElectricalRequest request, Electrical row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
     
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            return row;
        }

    }

}
