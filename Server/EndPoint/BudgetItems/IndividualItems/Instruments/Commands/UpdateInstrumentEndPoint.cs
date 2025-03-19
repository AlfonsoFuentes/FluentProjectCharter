using Server.Database.Entities.ProjectManagements;
using Server.EndPoint.BudgetItems.IndividualItems.Instruments.Commands;
using Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Commands;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Instruments.Commands
{
    public static class UpdateInstrumentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.Update, async (UpdateInstrumentRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Instrument>, IIncludableQueryable<Instrument, object>> Includes = x => x
                   .Include(x => x.Nozzles)
                   ;
                    Expression<Func<Instrument, bool>> Criteria = x => x.Id == Data.Id;

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);


                    if (row == null) { return Result.Fail(Data.NotFound); }
                    if (Data.GanttTaskId.HasValue)
                    {
                        var deliverable = await Repository.GetByIdAsync<GanttTask>(Data.GanttTaskId.Value);
                        if (deliverable != null)
                        {
                            deliverable.ShowBudgetItems = true;
                            await Repository.UpdateAsync(deliverable);
                        }

                    }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    if (Data.ShowDetails)
                    {
                        var equipmentTemplate = await GetInstrumentTemplate(Repository, Data);

                        if (row.InstrumentTemplateId != null && equipmentTemplate != null && equipmentTemplate.Id != row.InstrumentTemplateId.Value)
                        {
                            row.InstrumentTemplateId = equipmentTemplate.Id;
                        }
                        await UpdateNozzles(Repository, row, Data);
                    }


                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                var deliverable = row.GanttTaskId.HasValue ? StaticClass.GanttTasks.Cache.Key(row.GanttTaskId!.Value, row.ProjectId) : new[] { string.Empty };
                List<string> cacheKeys = [
                 ..StaticClass.BudgetItems.Cache.Key(row.Id, row.ProjectId, row.GanttTaskId),
                 ..deliverable
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
            async Task<InstrumentTemplate> GetInstrumentTemplate(IRepository Repository, UpdateInstrumentRequest Data)
            {
                Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x
                .Include(x => x.BrandTemplate)
                .Include(x => x.NozzleTemplates)
                ;
                Expression<Func<InstrumentTemplate, bool>> Criteria = x =>
                x.Brand == Data.Brand &&
                x.Model == Data.Model &&
                x.Type == Data.Type.Name &&
                x.TagLetter == Data.TagLetter &&
                x.SubType == Data.SubType.Name &&
                x.Reference == Data.Reference &&
                x.Material == Data.Material.Name &&
                x.SignalType == Data.SignalType.Name

                ;
                var equipmentTemplates = await Repository.GetAllAsync(Includes: Includes);

                var equipmentTemplate = equipmentTemplates.FirstOrDefault(Criteria.Compile());

                if (equipmentTemplate == null)
                {
                    equipmentTemplate = Template.AddInstrumentTemplate();

                    equipmentTemplate.BrandTemplateId = Data.BrandResponse.Id;//TODO: Add Brand
                    equipmentTemplate.Model = Data.Model;
                    equipmentTemplate.Type = Data.Type.Name;
                    equipmentTemplate.TagLetter = Data.TagLetter;
                    equipmentTemplate.SubType = Data.SubType.Name;
                    equipmentTemplate.Reference = Data.Reference;
                    equipmentTemplate.Material = Data.Material.Name;
                    equipmentTemplate.SignalType = Data.SignalType.Name;
                    equipmentTemplate.Value = Data.BudgetUSD;
                    foreach (var nozzle in Data.Nozzles)
                    {
                        var nozzleTemplate = NozzleTemplate.Create(equipmentTemplate.Id);
                        nozzleTemplate.ConnectionType = nozzle.ConnectionType.Name;
                        nozzleTemplate.NominalDiameter = nozzle.NominalDiameter.Name;
                        nozzleTemplate.NozzleType = nozzle.NozzleType.Name;
                        await Repository.AddAsync(nozzleTemplate);
                    }
                    await Repository.AddAsync(equipmentTemplate);
                }
                return equipmentTemplate;
            }

            async Task UpdateNozzles(IRepository Repository, Instrument row, UpdateInstrumentRequest Data)
            {
                if (Data.Nozzles != null)
                {
                    var nozzles = row.Nozzles.ToList();
                    foreach (var nozzle in Data.Nozzles)
                    {
                        var nozzleRow = nozzles.FirstOrDefault(x => x.Id == nozzle.Id);
                        if (nozzleRow == null)
                        {
                            nozzleRow = Nozzle.Create(row.Id);
                            nozzle.Map(nozzleRow);

                            await Repository.AddAsync(nozzleRow);
                        }
                        else
                        {
                            nozzle.Map(nozzleRow);
                            await Repository.UpdateAsync(nozzleRow);
                        }
                    }
                    var nozzlesToRemove = nozzles.Where(x => !Data.Nozzles.Select(x => x.Id).Contains(x.Id)).ToList();
                    await Repository.RemoveRangeAsync(nozzlesToRemove);
                }
                else
                {
                    await Repository.RemoveRangeAsync(row.Nozzles.ToList());
                }
            }
        }



        static Instrument Map(this UpdateInstrumentRequest request, Instrument row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;
            row.IsExisting = request.IsExisting;
            row.BudgetUSD = request.BudgetUSD;
            row.ProvisionalTag = request.ProvisionalTag;
            return row;
        }

    }

}
