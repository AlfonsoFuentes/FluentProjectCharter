using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Instruments.Validators;

namespace Server.EndPoint.Instruments.Validators
{
    public static class ValidateInstrumentsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.Validate, async (ValidateInstrumentRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Instrument, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Instrument, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Instruments.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
