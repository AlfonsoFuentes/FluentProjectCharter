using Shared.Models.OrganizationStrategies.Validators;
using Shared.Models.Templates.Pipes.Validators;

namespace Server.EndPoint.PipeTemplates.Validators
{
    public static class ValidatePipeTemplatesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.Validate, async (ValidatePipeTemplateRequest Data, IQueryRepository Repository) =>
                {
                    Func<IQueryable<PipeTemplate>, IIncludableQueryable<PipeTemplate, object>> Includes = x => x
                    .Include(x => x.BrandTemplate!)
                     ;


                    Expression<Func<PipeTemplate, bool>> CriteriaId = null!;
                    Func<PipeTemplate, bool> CriteriaExist = x => Data.Id == null ?
                    x.Material.Equals(Data.Material) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Insulation == Data.Insulation &&
                    x.Class.Equals(Data.Class) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Diameter.Equals(Data.Diameter) 
                    



                    : x.Id != Data.Id.Value &&
                      x.Material.Equals(Data.Material) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Insulation == Data.Insulation &&
                    x.Class.Equals(Data.Class) &&
                    x.Brand.Equals(Data.Brand) &&
                    x.Diameter.Equals(Data.Diameter);

                    string CacheKey = StaticClass.PipeTemplates.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId, Includes: Includes);
                });


            }
        }



    }

}
