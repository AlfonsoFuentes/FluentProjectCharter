using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Alterations.Requests;

namespace Server.EndPoint.Alterations.Commands
{

    public static class CreateAlterationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.Create, async (CreateAlterationRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Alteration>() == null || project.BudgetItems.OfType<Alteration>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Alteration.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Alterations.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Alteration Map(this CreateAlterationRequest request, Alteration row)
        {
            row.Name = request.Name;
            row.UnitaryCost = request.UnitaryCost;
            row.CostCenter = request.CostCenter.Name;
            row.Quantity = request.Quantity;
            row.Budget = request.Budget;
            return row;
        }

    }

}
