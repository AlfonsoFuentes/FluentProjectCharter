using Microsoft.AspNetCore.Components;
using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.DeliverableRisks.Responses;
using Shared.Models.Scopes.Responses;

namespace FluentWeb.Pages.DeliverableRisks;
#nullable disable
public partial class DeliverableRiskList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ScopeResponse Parent { get; set; } = new();


    DeliverableRiskResponse currentDeliverableRisk =>
 App.Project == null || App.Project.CurrentCase == null
 || App.Project.CurrentCase.CurrentScope == null ? null :
     FilteredItems.FirstOrDefault(x => x.Id == App.Project.CurrentCase.CurrentScope.CurrentDeliverableRisk!.Id);

    public List<DeliverableRiskResponse> Items => Parent == null ? new(): Parent.DeliverableRisks;
  

    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


 
    string nameFilter;
    public List<DeliverableRiskResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateDeliverableRisk/{Parent.ProjectId}/{Parent.Id}");

    }



    void Edit(DeliverableRiskResponse response)
    {
        Navigation.NavigateTo($"/UpdateDeliverableRisk/{response.Id}");
    }
    public async Task Delete(DeliverableRiskResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteDeliverableRiskRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.ProjectId,
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
