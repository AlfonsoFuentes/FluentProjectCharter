using Shared.Models.BudgetItems.Pipes.Validators;

namespace Server.EndPoint.Pipes.Validators
{
    public static class ValidatePipeTagsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.ValidateTag, async (ValidatePipeTagRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Pipe, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Pipe, bool> CriteriaExist = x => Data.Id == null ?
                    x.Tag.Equals(Data.Tag) : x.Id != Data.Id.Value && x.Tag.Equals(Data.Tag);
                    string CacheKey = StaticClass.Pipes.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
