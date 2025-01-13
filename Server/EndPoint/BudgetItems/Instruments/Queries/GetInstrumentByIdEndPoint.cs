using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Brands.Queries;
using Server.EndPoint.Projects.Queries;
using Shared.Enums.Materials;
using Shared.Models.BudgetItems.Equipments.Responses;
using Shared.Models.BudgetItems.Instruments.Records;
using Shared.Models.BudgetItems.Instruments.Responses;

namespace Server.EndPoint.Instruments.Queries
{
    public static class GetInstrumentByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.GetById, async (GetInstrumentByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Instrument>, IIncludableQueryable<Instrument, object>> Includes = x => x
                    .Include(x => x.Deliverable!)
                    .Include(x => x.InstrumentTemplate!);

                    ;

                    Expression<Func<Instrument, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Instruments.Cache.GetById(request.Id);
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
        public static InstrumentResponse Map(this Instrument row)
        {
            InstrumentResponse response = new()
            {
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId == null ? Guid.Empty : row.DeliverableId.Value,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                Budget = row.Budget,
                TagLetter = row.TagLetter,
                TagNumber = row.TagNumber,
                Tag = row.Tag,


            };
            response.Map(row.InstrumentTemplate);
            return response;
        }
        static InstrumentResponse Map(this InstrumentResponse response, InstrumentTemplate? row)
        {
            if (row == null)
            {
                return response;
            }

            response.Brand = row.BrandTemplate == null ? null : row.BrandTemplate.Map();
            response.Model = row.Model;
            response.VariableInstrument = row.Type;

            response.ModifierInstrument = row.SubType;
            response.Material = MaterialEnum.GetType(row.Material);
            response.Reference = row.Reference;
            response.SignalType = row.SignalType;

            return response;


        }

    }
}
