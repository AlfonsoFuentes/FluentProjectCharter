using Shared.Models.BudgetItems.IndividualItems.Pipes.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.Pipes.Commands
{

    public static class CreatePipeEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.Create, async (CreatePipeRequest data, IRepository repository) =>
                {
                    var project = await GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);
                    int order = GetNextOrder(project);

                    var row = Pipe.Create(project.Id);
                    row.Order = order;

                    data.Map(row);
                    if (data.ShowDetails)
                    {
                        var equipmentTemplate = await GetPipeTemplate(repository, data);

                        if (equipmentTemplate != null)
                        {
                            row.PipeTemplateId = equipmentTemplate.Id;


                        }
                        order = 0;
                        await CreateNozzles(repository, row, data);
                    }

                    await repository.AddAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
                  ..StaticClass.BudgetItems.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
            private async Task<Project?> GetProject(Guid ProjectId, IRepository repository)
            {
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x

                    .Include(d => d.BudgetItems)
                    ;

                Expression<Func<Project, bool>> criteria = d => d.Id == ProjectId;

                return await repository.GetAsync(Criteria: criteria, Includes: includes);
            }

            private int GetNextOrder(Project project)
            {
                var alterations = project.BudgetItems.OfType<Pipe>();
                return alterations.Any() ? alterations.Max(a => a.Order) + 1 : 1;
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
