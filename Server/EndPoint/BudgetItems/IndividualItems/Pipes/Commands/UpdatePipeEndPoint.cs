using Server.Database.Entities.ProjectManagements;
using Server.EndPoint.BudgetItems.IndividualItems.Nozzles.Commands;
using Server.EndPoint.BudgetItems.IndividualItems.Pipes.Commands;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Pipes.Commands
{
    public static class UpdatePipeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.Update, async (UpdatePipeRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Pipe>, IIncludableQueryable<Pipe, object>> Includes = x => x
                   .Include(x => x.Nozzles)
                   ;
                    Expression<Func<Pipe, bool>> Criteria = x => x.Id == Data.Id;

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
                        var equipmentTemplate = await GetPipeTemplate(Repository, Data);

                        if (row.PipeTemplateId != null && equipmentTemplate != null && equipmentTemplate.Id != row.PipeTemplateId.Value)
                        {
                            row.PipeTemplateId = equipmentTemplate.Id;
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
            async Task<PipeTemplate> GetPipeTemplate(IRepository Repository, UpdatePipeRequest Data)
            {
                Func<IQueryable<PipeTemplate>, IIncludableQueryable<PipeTemplate, object>> Includes = x => x
                 .Include(x => x.BrandTemplate)
                 .Include(x => x.NozzleTemplates)
                 ;
                Expression<Func<PipeTemplate, bool>> Criteria = x =>
                x.Brand == Data.Brand &&
               x.Insulation == Data.Insulation &&
               x.Material.Equals(Data.Material.Name) &&
               x.Diameter.Equals(Data.Diameter.Name) &&
               x.Class.Equals(Data.PipeClass.Name)

                ;
                var equipmentTemplates = await Repository.GetAllAsync(Includes: Includes);

                var equipmentTemplate = equipmentTemplates.FirstOrDefault(Criteria.Compile());

                if (equipmentTemplate == null)
                {
                    equipmentTemplate = Template.AddPipeTemplate();

                    equipmentTemplate.BrandTemplateId = Data.BrandResponse.Id;//TODO: Add Brand
                    equipmentTemplate.Material = Data.Material.Name;
                    equipmentTemplate.Diameter = Data.Diameter.Name;
                    equipmentTemplate.EquivalentLenghPrice = Data.EquivalentLenghPrice;
                    equipmentTemplate.LaborDayPrice = Data.LaborDayPrice;
                    equipmentTemplate.Class = Data.PipeClass.Name;
                    equipmentTemplate.Insulation = Data.Insulation;
                    await Repository.AddAsync(equipmentTemplate);
                }
                return equipmentTemplate;
            }

            async Task UpdateNozzles(IRepository Repository, Pipe row, UpdatePipeRequest Data)
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



        static Pipe Map(this UpdatePipeRequest request, Pipe row)
        {
            row.Name = request.Name;
            row.BudgetUSD = request.BudgetUSD;
            row.FluidCodeId = request.FluidCode == null ? null : request.FluidCode.Id;
            row.LaborQuantity = request.LaborQuantity;
            row.MaterialQuantity = request.MaterialQuantity;
            row.TagNumber = request.TagNumber;
            row.Material = request.Material.Name;
            row.Diameter = request.Diameter.Name;
            row.FluidCodeCode = request.FluidCodeCode;
            row.Insulation = request.Insulation;
            row.LaborDayPrice = request.LaborDayPrice;
            row.EquivalentLenghPrice = request.EquivalentLenghPrice;
            row.IsExisting = request.IsExisting;
            return row;
        }

    }

}
