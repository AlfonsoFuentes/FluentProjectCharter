using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Records;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Requests;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Responses;


#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.Taxs;
public partial class CreateTax
{
    [CascadingParameter]
    public App App { get; set; }
    CreateTaxRequest Model = new();

    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid? GanttTaskId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetTaxItems();

        Model.GanttTaskId = GanttTaskId;
        Model.ProjectId = ProjectId;
    }
    TaxItemResponseList taxItemResponseList = new();

    async Task GetTaxItems()
    {
        var result = await GenericService.GetAll<TaxItemResponseList, GetBudgetItemsToApplyTaxRequest>(new GetBudgetItemsToApplyTaxRequest() { ProjectId = App.ProjectId });
        if (result.Succeeded)
        {
            taxItemResponseList = result.Data;

        }
    }

}
