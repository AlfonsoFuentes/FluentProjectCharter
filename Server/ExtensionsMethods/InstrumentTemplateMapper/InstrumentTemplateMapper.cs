using Server.EndPoint.Brands.Queries;
using Server.ExtensionsMethods.InstrumentTemplateMapper;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;
using Shared.Models.Templates.Instruments.Responses;

namespace Server.ExtensionsMethods.InstrumentTemplateMapper
{
    public static class InstrumentTemplateMapper
    {
        public static InstrumentTemplate Map(this InstrumentTemplateResponse request, InstrumentTemplate row)
        {
            row.Value = request.Value;
            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.Material = request.Material.Id;

            row.Model = request.Model;
            row.BrandTemplateId = request.Brand!.Id;
            row.ModifierVariable = request.ModifierVariable.Id;
            row.Variable = request.VariableInstrument.Id;
            row.SignalType = request.SignalType.Id;

            row.ConnectionType = request.ConnectionType.Id;

            return row;
        }
        public static InstrumentTemplateResponse Map(this InstrumentTemplate row)
        {
            return new()
            {
                Id = row.Id,
                Brand = row.BrandTemplate == null ? new() : row.BrandTemplate.Map(),
                Material = MaterialEnum.GetType(row.Material),

                Model = row.Model,
                Reference = row.Reference,
                ModifierVariable = ModifierVariableInstrumentEnum.GetType(row.ModifierVariable),

                VariableInstrument = VariableInstrumentEnum.GetType(row.Variable),
                Value = row.Value,
                SignalType = SignalTypeEnum.GetType(row.SignalType),
                Nozzles = row.NozzleTemplates.Count == 0 ? new() : row.NozzleTemplates.Select(x => x.Map()).ToList(),

                ConnectionType = ConnectionTypeEnum.GetType(row.ConnectionType),

            };
        }


        public static InstrumentTemplate Map(this InstrumentResponse request, InstrumentTemplate row, double Value)
        {

            row.Value = Value;

            row.TagLetter = request.TagLetter;
            row.Reference = request.Reference;
            row.Material = request.Material.Id;

            row.Model = request.Model;
            row.BrandTemplateId = request.Brand!.Id;
            row.ModifierVariable = request.ModifierVariable.Id;
            row.Variable = request.VariableInstrument.Id;
            row.SignalType = request.SignalType.Id;

            row.ConnectionType = request.ConnectionType.Id;

            return row;
        }
        public static Instrument Map(this InstrumentResponse request, Instrument row)
        {
            row.Name = request.Name;
            row.TagLetter = request.TagLetter;
            row.TagNumber = request.TagNumber;
            row.IsExisting = request.IsExisting;
            row.BudgetUSD = request.BudgetUSD;
            row.ProvisionalTag = request.ProvisionalTag;
            return row;
        }
        public static async Task<InstrumentTemplate> GetInstrumentTemplate(IRepository Repository, InstrumentResponse data)
        {
            Func<IQueryable<InstrumentTemplate>, IIncludableQueryable<InstrumentTemplate, object>> Includes = x => x

                .Include(x => x.NozzleTemplates)
                  ;

            Expression<Func<InstrumentTemplate, bool>> criteria = it =>
            it.Material == data.Material.Id &&
            it.ConnectionType == data.ConnectionType.Id &&
            it.SignalType == data.SignalType.Id &&
            it.Variable == data.VariableInstrument.Id &&
            it.ModifierVariable == data.ModifierVariable.Id &&
            it.BrandTemplateId == data.BrandId &&
            it.Model.Equals(data.Model) &&
            it.Reference.Equals(data.Reference) 
            ;
            var instrumentTemplates = await Repository.GetAllAsync(Includes: Includes, Criteria: criteria);
            if (instrumentTemplates != null && instrumentTemplates.Any())
            {
                foreach (var item in instrumentTemplates)
                {

                    if (item.NozzleTemplates.ValidateNozzles(data.Nozzles))
                    {
                        return item; // Si todas las boquillas coinciden, retornar true
                    }
                }

            }

            var instrumentTemplate = Template.AddInstrumentTemplate();
            data.Map(instrumentTemplate, data.BudgetUSD);
            foreach (var nozzle in data.Nozzles)
            {
                var nozzleTemplate = NozzleTemplate.Create(instrumentTemplate.Id);
                nozzle.Map(nozzleTemplate);
                await Repository.AddAsync(nozzleTemplate);
            }
            await Repository.AddAsync(instrumentTemplate);
            return instrumentTemplate;

        }

    }
}
