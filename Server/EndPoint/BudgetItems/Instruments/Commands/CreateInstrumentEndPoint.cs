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
                    if (Data.ShowDetails)
                    {
                        var equipmentTemplate = await GetInstrumentTemplate(Repository, Data);

                        if (equipmentTemplate != null)
                        {
                            row.InstrumentTemplateId = equipmentTemplate.Id;

                           
                        }
                        order = 0;
                        await CreateNozzles(Repository, row, Data);
                    }
                    

                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId),
                        .. StaticClass.Instruments.Cache.Key(row.Id),StaticClass.InstrumentTemplates.Cache.GetAll];

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
                    equipmentTemplate.SignalType= Data.SignalType.Name;
                    equipmentTemplate.Value = Data.Budget;
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
            async Task CreateNozzles(IRepository Repository, Instrument row, CreateInstrumentRequest Data)
            {
                int order = 0;
                foreach (var nozzleTempalte in Data.Nozzles)
                {
                    order++;
                    var nozzle = Nozzle.Create(row.Id);
                    nozzle.NozzleType = nozzleTempalte.NozzleType.Name;
                    nozzle.ConnectionType = nozzleTempalte.ConnectionType.Name;
                    nozzle.NominalDiameter = nozzleTempalte.NominalDiameter.Name;
                    nozzle.Order = order;
                    await Repository.AddAsync(nozzle);
                }
            }
        }


        static Instrument Map(this CreateInstrumentRequest request, Instrument row)
        {
            row.Name = request.Name;
            row.TagLetter = request.ShowDetails ? request.TagLetter : string.Empty;
            row.TagNumber = request.TagNumber;
            row.IsExisting = request.IsExisting;
            row.Budget = request.Budget;
            row.ProvisionalTag = request.ProvisionalTag;
            return row;
        }

    }

}
