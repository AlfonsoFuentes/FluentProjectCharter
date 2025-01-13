using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.EngineeringDesigns.Requests;

namespace Server.EndPoint.EngineeringDesigns.Commands
{

    public static class CreateEngineeringDesignEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringDesigns.EndPoint.Create, async (CreateEngineeringDesignRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<EngineeringDesign>() == null || project.BudgetItems.OfType<EngineeringDesign>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = EngineeringDesign.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.EngineeringDesigns.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static EngineeringDesign Map(this CreateEngineeringDesignRequest request, EngineeringDesign row)
        {
            row.Name = request.Name;
      
            row.Budget = request.Budget;
            return row;
        }

    }

}
