using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.KnownRisks.Requests;
using Shared.Models.KnownRisks.Responses;

namespace FluentWeb.Pages.KnownRisks;
#nullable disable
public partial class KnownRiskList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }



    public List<KnownRiskResponse> Items => Parent == null ? new() : Parent.KnownRisks;
    string nameFilter;
    public List<KnownRiskResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateKnownRisk/{Parent.Id}/{Parent.ProjectId}");

    }

    void Edit(KnownRiskResponse response)
    {
        Navigation.NavigateTo($"/UpdateKnownRisk/{response.Id}/{Parent.ProjectId}");
    }


   
    public async Task Delete(KnownRiskResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteKnownRiskRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
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
