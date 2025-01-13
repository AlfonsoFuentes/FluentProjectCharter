using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Assumptions.Requests;
using Shared.Models.Assumptions.Responses;
using Web.Infrastructure.Managers.Generic;
using Shared.Models.Scopes.Responses;
using Shared.Models.Deliverables.Responses;

namespace FluentWeb.Pages.Assumptions;
#nullable disable
public partial class AssumptionList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public List<AssumptionResponse> Items { get; set; }
    string nameFilter;
    public List<AssumptionResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateAssumption/{DeliverableId}/{ProjectId}");
        
    }


    void Edit(AssumptionResponse response)
    {
        Navigation.NavigateTo($"/UpdateAssumption/{response.Id}/{ProjectId}");
    }
    public async Task Delete(AssumptionResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteAssumptionRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = ProjectId,
            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll.Invoke();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
