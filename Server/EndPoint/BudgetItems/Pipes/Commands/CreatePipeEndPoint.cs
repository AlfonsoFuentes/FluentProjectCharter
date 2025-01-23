using Shared.Models.BudgetItems.Pipes.Requests;

namespace Server.EndPoint.Pipes.Commands
{

    public static class CreatePipeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.Create, async (CreatePipeRequest Data, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x.Include(x => x.BudgetItems);
                    Expression<Func<Project, bool>> Criteria = x => x.Id == Data.ProjectId;

                    var project = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);
                    int order = 1;
                    if (project != null)
                    {
                        order = project.BudgetItems.OfType<Pipe>() == null ||
                        project.BudgetItems.OfType<Pipe>().Count() == 0 ?
                        order : project.BudgetItems.OfType<Pipe>().MaxBy(x => x.Order)!.Order + 1;
                    }

                    var row = Pipe.Create(Data.ProjectId, Data.DeliverableId);

                    await Repository.AddAsync(row);

                    row.Order = order;

                    Data.Map(row);
                    if (Data.ShowDetails)
                    {
                        var equipmentTemplate = await GetPipeTemplate(Repository, Data);

                        if (equipmentTemplate != null)
                        {
                            row.PipeTemplateId = equipmentTemplate.Id;


                        }
                        order = 0;
                        await CreateNozzles(Repository, row, Data);
                    }


                    List<string> cache = [.. StaticClass.Projects.Cache.Key(Data.ProjectId),
                        .. StaticClass.Pipes.Cache.Key(row.Id),StaticClass.PipeTemplates.Cache.GetAll];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            async Task<PipeTemplate> GetPipeTemplate(IRepository Repository, CreatePipeRequest Data)
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
               x.Class.Equals(Data.PipeClass.Name);
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
            async Task CreateNozzles(IRepository Repository, Pipe row, CreatePipeRequest Data)
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


        static Pipe Map(this CreatePipeRequest request, Pipe row)
        {
            row.Name = request.Name;
            row.Budget = request.Budget;
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
