using Shared.Models.Templates.NozzleTemplates;
using Shared.Models.Templates.Valves.Requests;

namespace Server.EndPoint.ValveTemplates.Commands
{
    public static class UpdateValveTemplateEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ValveTemplates.EndPoint.Update, async (UpdateValveTemplateRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<ValveTemplate>, IIncludableQueryable<ValveTemplate, object>> Includes = x =>
                    x.Include(x => x.NozzleTemplates)
                    .Include(x => x.BrandTemplate!);
                    ;

                    Expression<Func<ValveTemplate, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);

                    Data.Map(row);

                    await UpdateNozzles(Repository, row, Data);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.ValveTemplates.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }

            async Task UpdateNozzles(IRepository repository, ValveTemplate row, UpdateValveTemplateRequest Data)
            {
                var updatedNozzleIds = Data.Nozzles.Select(n => n.Id).ToList();

                var existingNozzles = row.NozzleTemplates;
                // Eliminar las boquillas que ya no se requieren
                var nozzlesToRemove = existingNozzles.Where(n => !updatedNozzleIds.Contains(n.Id)).ToList();
                if (nozzlesToRemove.Any())
                {
                    await repository.RemoveRangeAsync(nozzlesToRemove);
                }

                // Actualizar o agregar las boquillas nuevas
                foreach (var nozzleRequest in Data.Nozzles)
                {
                    var existingNozzle = existingNozzles.FirstOrDefault(n => n.Id == nozzleRequest.Id);
                    if (existingNozzle != null)
                    {
                        // Actualizar la boquilla existente
                        nozzleRequest.Map(existingNozzle);
                        await repository.UpdateAsync(existingNozzle);
                    }
                    else
                    {
                        // Agregar una nueva boquilla
                        var newNozzle = NozzleTemplate.Create(row.Id);
                        nozzleRequest.Map(newNozzle);
                        await repository.AddAsync(newNozzle);
                    }
                }
            }

        }


        static ValveTemplate Map(this UpdateValveTemplateRequest request, ValveTemplate row)
        {
            row.Value = request.Value;
            row.TagLetter = request.TagLetter;

            row.Model = request.Model;
            row.BrandTemplateId = request.BrandResponse!.Id;

            row.Type = request.Type.Name;
            row.HasFeedBack = request.HasFeedBack;
            row.Diameter = request.Diameter.Name;
            row.ActuatorType = request.ActuatorType.Name;
            row.FailType = request.FailType.Name;
            row.Material=request.Material.Name;
            row.PositionerType = request.PositionerType.Name;
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
