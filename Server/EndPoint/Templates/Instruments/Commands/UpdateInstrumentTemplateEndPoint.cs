using Shared.Models.Templates.Instruments.Requests;
using Shared.Models.Templates.NozzleTemplates;

namespace Server.EndPoint.Templates.Instruments.Commands
{
    public static class UpdateInstrumentTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.InstrumentTemplates.EndPoint.Update, async (UpdateInstrumentTemplateRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<InstrumentTemplate>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

                    Data.Map(row);

                    await UpdateNozzlesAsync(Repository, Data);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.InstrumentTemplates.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private async Task UpdateNozzlesAsync(IRepository repository, UpdateInstrumentTemplateRequest data)
            {
                Expression<Func<NozzleTemplate, bool>> criteria = x => x.TemplateId == data.Id;
                var nozzles = await repository.GetAllAsync(Criteria: criteria);

                var nozzlesToRemove = nozzles.Where(n => !data.Nozzles.Any(x => x.Id == n.Id)).ToList();
                foreach (var nozzle in nozzlesToRemove)
                {
                    await repository.RemoveAsync(nozzle);
                }

                foreach (var item in data.Nozzles)
                {
                    var nozzle = await repository.GetByIdAsync<NozzleTemplate>(item.Id);
                    if (nozzle != null)
                    {
                        item.Map(nozzle);
                        await repository.UpdateAsync(nozzle);
                    }
                    else
                    {
                        nozzle = NozzleTemplate.Create(data.Id);
                        item.Map(nozzle);
                        await repository.AddAsync(nozzle);
                    }
                }
            }
        }


        static InstrumentTemplate Map(this UpdateInstrumentTemplateRequest request, InstrumentTemplate row)
        {
            row.Value = request.Value;
            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.Material = request.Material.Name;

            row.Model = request.Model;
            row.BrandTemplateId = request.BrandResponse!.Id;
            row.SubType = request.SubType.Name;
            row.Type = request.Type.Name;
            row.SignalType = request.SignalType.Name;


            return row;
        }
        static NozzleTemplate Map(this NozzleTemplateResponse request, NozzleTemplate row)
        {
            row.NominalDiameter = request.NominalDiameter.Name;
            row.ConnectionType = request.ConnectionType.Name;
            row.NozzleType = request.NozzleType.Name;
            return row;
        }
    }

}
