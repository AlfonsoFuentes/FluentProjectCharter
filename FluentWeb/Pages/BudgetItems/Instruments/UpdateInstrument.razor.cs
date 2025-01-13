using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Instruments.Records;
using Shared.Models.BudgetItems.Instruments.Requests;
using Shared.Models.BudgetItems.Instruments.Responses;

namespace FluentWeb.Pages.BudgetItems.Instruments;
public partial class UpdateInstrument
{
    UpdateInstrumentRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<InstrumentResponse, GetInstrumentByIdRequest>(
            new GetInstrumentByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                Budget = result.Data.Budget,
                Material = result.Data.Material,
                Model = result.Data.Model,
                Reference = result.Data.Reference,
                TagLetter = result.Data.TagLetter,
                TagNumber = result.Data.TagNumber,
                Brand = result.Data.Brand,
              
                SignalType = result.Data.SignalType,
                VariableInstrument = result.Data.VariableInstrument,
                ModifierInstrument = result.Data.ModifierInstrument,


            };
        }
    }
}
