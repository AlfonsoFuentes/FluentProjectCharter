using Server.EndPoint.Brands.Queries;
using Server.EndPoint.Templates.Pipes.Queries;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Templates.NozzleTemplates;
using Shared.Models.Templates.Pipings.Records;
using Shared.Models.Templates.Pipings.Responses;

namespace Server.EndPoint.Templates.Pipes.Queries
{
    public static class GetPipeTemplateByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.PipeTemplates.EndPoint.GetById,
                    async (GetPipeTemplateByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<PipeTemplate>, IIncludableQueryable<PipeTemplate, object>> Includes = x =>
                    x.Include(x => x.NozzleTemplates)
                    .Include(x => x.BrandTemplate!);


                    Expression<Func<PipeTemplate, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.PipeTemplates.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }


        public static PipeTemplateResponse Map(this PipeTemplate row)
        {
            return new()
            {
                Id = row.Id,
                BrandResponse = row.BrandTemplate == null ? new() : row.BrandTemplate.Map(),
                Material = MaterialEnum.GetType(row.Material),
                Class = PipeClassEnum.GetType(row.Class),
                Diameter = NominalDiameterEnum.GetType(row.Diameter),
                EquivalentLenghPrice = row.EquivalentLenghPrice,
                Insulation = row.Insulation,
                LaborDayPrice = row.LaborDayPrice,



            };
        }

    }
}
