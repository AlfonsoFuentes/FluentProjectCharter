using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Responses;
using Shared.Models.AcceptanceCriterias.Requests;
using Shared.Models.AcceptanceCriterias.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.AcceptanceCriterias;
#nullable disable
public partial class AcceptanceCriteriaList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid DeliverableId { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid ProjectId { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Inject]
    private IGenericService Service { get; set; } = null!;
    [Parameter]
    [EditorRequired]
    public List<AcceptanceCriteriaResponse> Items { get; set; }
    string nameFilter;
    public List<AcceptanceCriteriaResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateAcceptanceCriteria/{DeliverableId}/{ProjectId}");

    }

    void Edit(AcceptanceCriteriaResponse response)
    {
        Navigation.NavigateTo($"/UpdateAcceptanceCriteria/{response.Id}/{ProjectId}");
    }
    public async Task Delete(AcceptanceCriteriaResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteAcceptanceCriteriaRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = ProjectId,
            };
            var resultDelete = await Service.Delete(request);
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
