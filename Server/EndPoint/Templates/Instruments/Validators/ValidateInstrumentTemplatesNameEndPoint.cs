using Shared.Models.OrganizationStrategies.Validators;
using Shared.Models.Templates.Instruments.Validators;

namespace Server.EndPoint.InstrumentTemplates.Validators
{
    public static class ValidateInstrumentTemplatesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.Validate, async (ValidateInstrumentTemplateRequest Data, IQueryRepository Repository) =>
                {
                    Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x
                    .Include(x => x.BrandTemplate!)
                     ;


                    Expression<Func<InstrumentTemplate, bool>> CriteriaId = null!;
                    Func<InstrumentTemplate, bool> CriteriaExist = x =>
                        (Data.Id == null || x.Id != Data.Id.Value) &&
                        x.Material.Equals(Data.Material, StringComparison.OrdinalIgnoreCase) &&
                        x.Brand.Equals(Data.Brand, StringComparison.OrdinalIgnoreCase) &&
                        x.Model.Equals(Data.Model, StringComparison.OrdinalIgnoreCase) &&
                        x.Type.Equals(Data.Type, StringComparison.OrdinalIgnoreCase) &&
                        x.SubType.Equals(Data.SubType, StringComparison.OrdinalIgnoreCase) &&
                        x.TagLetter.Equals(Data.TagLetter, StringComparison.OrdinalIgnoreCase) &&
                        x.Reference.Equals(Data.Reference, StringComparison.OrdinalIgnoreCase) &&
                        x.SignalType.Equals(Data.SignalType, StringComparison.OrdinalIgnoreCase);

                    string CacheKey = StaticClass.InstrumentTemplates.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId, Includes: Includes);
                });


            }
        }



    }

}
