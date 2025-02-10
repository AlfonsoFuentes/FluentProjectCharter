using Shared.Models.BudgetItems.IndividualItems.Equipments.Validators;

namespace Server.EndPoint.BudgetItems.IndividualItems.Equipments.Validators
{
    public static class ValidateEquipmentTagsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.ValidateTag, async (ValidateEquipmentTagRequest Data, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                       .Include(x => x.BudgetItems)
                       ;
                    Expression<Func<Project, bool>> CriteriaDeliverable = x => x.Id == Data.ProjectId;
                    string CacheKey = StaticClass.Projects.Cache.GetById(Data.ProjectId);

                    var project = await Repository.GetAsync(Cache: CacheKey, Criteria: CriteriaDeliverable, Includes: Includes);
                    if (project == null) return false;
                    var budgetitems = project.BudgetItems.OfType<Equipment>();
                    if (budgetitems == null) return false;


                    Func<Equipment, bool> CriteriaExist = x => Data.Id == null ?
                    x.Tag.Equals(Data.Tag) : x.Id != Data.Id.Value && x.Tag.Equals(Data.Tag);

                    var result = budgetitems.Any(x => CriteriaExist(x));

                    return result;
                });


            }
        }



    }

}
