using Shared.Models.Templates.Valves.Validators;

namespace Server.EndPoint.Templates.Valves.Validators
{
    public static class ValidateValveTemplatesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.Validate, async (ValidateValveTemplateRequest Data, IQueryRepository Repository) =>
                {
                    Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x => x
                    .Include(x => x.BrandTemplate!)
                     ;


                    Expression<Func<ValveTemplate, bool>> CriteriaId = null!;
                    Func<ValveTemplate, bool> CriteriaExist = x => Data.Id == null ?
                    x.Material.Equals(Data.Material) &&
                    x.SignalType.Equals(Data.SignalType) &&
                    x.FailType.Equals(Data.FailType) &&
                    x.Type.Equals(Data.Type) &&
                    x.ActuatorType.Equals(Data.ActuadorType) &&
                    x.Diameter.Equals(Data.Diameter) &&
                    x.HasFeedBack == Data.HasFeedBack &&
                    x.PositionerType.Equals(Data.PositionerType) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Model.Equals(Data.Model) &&
                    x.TagLetter.Equals(Data.TagLetter)

                    : x.Id != Data.Id.Value &&
                   x.Material.Equals(Data.Material) &&
                    x.SignalType.Equals(Data.SignalType) &&
                    x.FailType.Equals(Data.FailType) &&
                    x.Type.Equals(Data.Type) &&
                    x.ActuatorType.Equals(Data.ActuadorType) &&
                    x.Diameter.Equals(Data.Diameter) &&
                    x.HasFeedBack == Data.HasFeedBack &&
                    x.PositionerType.Equals(Data.PositionerType) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Model.Equals(Data.Model) &&
                    x.TagLetter.Equals(Data.TagLetter)
                    ;

                    string CacheKey = StaticClass.ValveTemplates.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId, Includes: Includes);
                });


            }
        }



    }

}
