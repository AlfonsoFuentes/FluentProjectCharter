using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Taxs.Requests;

namespace Server.EndPoint.Taxs.Commands
{

    public static class CreateTaxEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Taxs.EndPoint.Create, async (CreateTaxRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Tax>() == null || project.BudgetItems.OfType<Tax>().Count() == 0 ?
                        order : project.BudgetItems.MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Tax.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);
                    foreach (var item in Data.TaxItems)
                    {
                        var taxitem = TaxesItem.Create(row.Id, item.BudgetItemId!.Value);
                        await Repository.AddAsync(taxitem);

                    }

                    row.Order = order;

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Taxs.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Tax Map(this CreateTaxRequest request, Tax row)
        {
            row.Name = request.Name;
            row.Percentage = request.Percentage;
            row.Budget = request.Budget;
            return row;
        }

    }

}
