using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Paintings.Validators;

namespace Server.EndPoint.Paintings.Validators
{
    public static class ValidatePaintingsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Paintings.EndPoint.Validate, async (ValidatePaintingRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Painting, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Painting, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Paintings.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
