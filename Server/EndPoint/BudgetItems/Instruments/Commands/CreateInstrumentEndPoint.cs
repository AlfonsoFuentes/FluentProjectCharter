using Server.Database.Entities.BudgetItems.Commons;
using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Enums;
using Shared.Models.BudgetItems.Equipments.Requests;
using Shared.Models.BudgetItems.Instruments.Requests;

namespace Server.EndPoint.Instruments.Commands
{

    public static class CreateInstrumentEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Instruments.EndPoint.Create, async (CreateInstrumentRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Instrument>() == null || 
                        project.BudgetItems.OfType<Instrument>().Count() == 0 ?
                        order : project.BudgetItems.OfType<Instrument>().MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Instrument.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);


                    row.Order = order;

                    Data.Map(row);
                    var instrumentTemplate = await GetInstrumentTemplate(Repository, Data);

                    if (instrumentTemplate != null)
                    {
                        row.InstrumentTemplateId = instrumentTemplate.Id;
                    }
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Instruments.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            async Task<InstrumentTemplate> GetInstrumentTemplate(IRepository Repository, CreateInstrumentRequest Data)
            {
                Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x
                .Include(x => x.BrandTemplate)
                .Include(x => x.NozzleTemplates)
                ;
                Expression<Func<InstrumentTemplate, bool>> Criteria = x =>
                x.Brand == Data.Brand!.Name &&
                x.Model == Data.Model &&
                x.Type == Data.VariableInstrument &&
           
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


        static Instrument Map(this CreateInstrumentRequest request, Instrument row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;
 
            row.Budget = request.Budget;
            return row;
        }

    }

}
