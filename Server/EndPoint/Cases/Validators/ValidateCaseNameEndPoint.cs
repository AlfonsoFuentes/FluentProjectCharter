

using Server.Database.Entities;
using Shared.Models.Cases.Validators;

namespace Server.EndPoint.Cases.Validators
{
    public static class ValidateCaseNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Cases.EndPoint.Validate, async (ValidateCaseRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Case, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Case, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Cases.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
