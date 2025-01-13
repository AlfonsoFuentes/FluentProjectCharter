using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Enums;
using Shared.Models.BudgetItems.Instruments.Requests;


namespace Server.EndPoint.Instruments.Commands
{
    public static class UpdateInstrumentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.Update, async (UpdateInstrumentRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Instrument>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);

                    var instrumentTemplate = await GetInstrumentTemplate(Repository, Data);

                    if (row.InstrumentTemplateId != null && instrumentTemplate != null && instrumentTemplate.Id != row.InstrumentTemplateId.Value)
                    {
                        row.InstrumentTemplateId = instrumentTemplate.Id;
                    }

                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Instruments.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            async Task<InstrumentTemplate> GetInstrumentTemplate(IRepository Repository, UpdateInstrumentRequest Data)
            {
                Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x
                .Include(x => x.BrandTemplate)
                .Include(x => x.NozzleTemplates)
                ;
                Expression<Func<InstrumentTemplate, bool>> Criteria = x =>
                x.Brand == Data.Brand!.Name &&
                x.Model == Data.Model &&
        
                x.SubType == Data.ModifierInstrument &&
            
               x.Material == Data.Material.Name &&
                x.SignalType == Data.SignalType

                ;
                var equipmentTemplate = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                if (equipmentTemplate == null)
                {
                    equipmentTemplate = InstrumentTemplate.AddInstrumentTemplate();

                    equipmentTemplate.BrandTemplateId = Data.Brand!.Id;//TODO: Add Brand
                    equipmentTemplate.Model = Data.Model;
                    equipmentTemplate.Type = Data.VariableInstrument;
                  
                    equipmentTemplate.SubType = Data.ModifierInstrument;
                    equipmentTemplate.Reference = Data.Reference;
                    equipmentTemplate.Material = Data.Material.Name;
                    equipmentTemplate.SignalType = Data.SignalType;

                    equipmentTemplate.Value = Data.Budget;

                    await Repository.AddAsync(equipmentTemplate);
                }
                return equipmentTemplate;
            }
        }


        static Instrument Map(this UpdateInstrumentRequest request, Instrument row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;

   
            row.Budget = request.Budget;
            
            return row;
        }

    }

}
