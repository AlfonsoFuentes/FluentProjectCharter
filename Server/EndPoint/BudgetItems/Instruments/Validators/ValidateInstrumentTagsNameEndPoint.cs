using Shared.Models.BudgetItems.Instruments.Validators;

namespace Server.EndPoint.Instruments.Validators
{
    public static class ValidateInstrumentTagsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.ValidateTag, async (ValidateInstrumentTagRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Instrument, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Instrument, bool> CriteriaExist = x => Data.Id == null ?
                    x.Tag.Equals(Data.Tag) : x.Id != Data.Id.Value && x.Tag.Equals(Data.Tag);
                    string CacheKey = StaticClass.Instruments.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
