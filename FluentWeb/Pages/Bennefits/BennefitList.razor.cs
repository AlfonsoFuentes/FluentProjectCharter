using Microsoft.AspNetCore.Components;
using Shared.Models.Bennefits.Requests;
using Shared.Models.Bennefits.Responses;
using Shared.Models.Scopes.Responses;

namespace FluentWeb.Pages.Bennefits;
#nullable disable
public partial class BennefitList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]

    [EditorRequired]
    public ScopeResponse Parent { get; set; } = new();


    BennefitResponse currentConstraint =>
 App.Project == null || App.Project.CurrentCase == null
 || App.Project.CurrentCase.CurrentScope == null ? null :
     FilteredItems.FirstOrDefault(x => x.Id == App.Project.CurrentCase.CurrentScope.CurrentBennefit!.Id);

    public List<BennefitResponse> Items => Parent == null ? new() : Parent.Bennefits;
  
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
  
    string nameFilter;
    public List<BennefitResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateBennefit/{Parent.ProjectId}/{Parent.Id}");

    }



    void Edit(BennefitResponse response)
    {
        Navigation.NavigateTo($"/UpdateBennefit/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(BennefitResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteBennefitRequest request = new()
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
