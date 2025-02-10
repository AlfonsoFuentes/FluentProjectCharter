using Shared.Models.BudgetItems.IndividualItems.Pipes.Validators;

namespace Server.EndPoint.BudgetItems.IndividualItems.Pipes.Validators
{
    public static class ValidatePipeTagsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.ValidateTag, async (ValidatePipeTagRequest Data, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                        .Include(x => x.BudgetItems)
                        ;
                    Expression<Func<Project, bool>> CriteriaDeliverable = x => x.Id == Data.ProjectId;
                    string CacheKey = StaticClass.Projects.Cache.GetById(Data.ProjectId);

                    var project = await Repository.GetAsync(Cache: CacheKey, Criteria: CriteriaDeliverable, Includes: Includes);
                    if (project == null) return false;
                    var budgetitems = project.BudgetItems.OfType<Pipe>();
                    if (budgetitems == null) return false;


                    Func<Pipe, bool> CriteriaExist = x => Data.Id == null ?
                    x.Tag.Equals(Data.Tag) : x.Id != Data.Id.Value && x.Tag.Equals(Data.Tag);

                    var result = budgetitems.Any(x => CriteriaExist(x));

                    return result;
                });


            }
        }



    }

}
