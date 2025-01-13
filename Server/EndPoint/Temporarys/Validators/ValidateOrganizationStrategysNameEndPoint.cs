using Shared.Models.OrganizationStrategies.Validators;
using Shared.Models.Temporarys.Validators;

namespace Server.EndPoint.Temporarys.Validators
{
    //public static class ValidateTemporarysNameEndPoint
    //{
    //    public class EndPoint : IEndPoint
    //    {
    //        public void MapEndPoint(IEndpointRouteBuilder app)
    //        {
    //            app.MapPost(StaticClass.Temporarys.EndPoint.Validate, async (ValidateTemporaryRequest Data, IQueryRepository Repository) =>
    //            {
    //                Expression<Func<Temporary, bool>> CriteriaId = null!;
    //                Func<Temporary, bool> CriteriaExist = x => Data.Id == null ?
    //                x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
    //                string CacheKey = StaticClass.Temporarys.Cache.GetAll;

    //                return true;// await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
    //            });


    //        }
    //    }



    //}

}
