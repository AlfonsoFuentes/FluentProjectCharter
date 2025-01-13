using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Pipings.Requests;

namespace Server.EndPoint.Pipings.Commands
{

    public static class CreatePipingEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipings.EndPoint.Create, async (CreatePipingRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Isometric>() == null ||
                        project.BudgetItems.OfType<Isometric>().Count() == 0 ?
                        order : project.BudgetItems.OfType<Isometric>().MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Isometric.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    
                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Pipings.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Isometric Map(this CreatePipingRequest request, Isometric row)
        {
            row.Name = request.Name;
            row.MaterialUnitaryCost = request.MaterialUnitaryCost;
            row.MaterialQuantity = request.MaterialQuantity;
            row.LaborUnitaryCost = request.LaborUnitaryCost;
            row.LaborQuantity = request.LaborQuantity;
            row.Diameter = request.Diameter;
            row.Material = request.Material;
            row.Insulation = request.Insulation;
            row.FluidCodeName = request.FluidCodeName;
            row.TagNumber = request.TagNumber;
            row.Budget = request.Budget;

          
            return row;
        }

    }

}
