using Shared.Models.BudgetItems.Valves.Validators;

namespace Server.EndPoint.Valves.Validators
{
    public static class ValidateValveTagsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Valves.EndPoint.ValidateTag, async (ValidateValveTagRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Valve, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Valve, bool> CriteriaExist = x => Data.Id == null ?
                    x.Tag.Equals(Data.Tag) : x.Id != Data.Id.Value && x.Tag.Equals(Data.Tag);
                    string CacheKey = StaticClass.Valves.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
