using Shared.Models.OrganizationStrategies.Validators;

namespace Server.EndPoint.OrganizationStrategys.Vallidators
{
    public static class ValidateOrganizationStrategysNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.OrganizationStrategys.EndPoint.Validate, async (ValidateOrganizationStrategyRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<OrganizationStrategy, bool>> CriteriaId = null!;
                    Func<OrganizationStrategy, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.OrganizationStrategys.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
