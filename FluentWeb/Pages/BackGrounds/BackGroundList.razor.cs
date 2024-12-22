using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Backgrounds.Requests;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Cases.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.BackGrounds;
#nullable disable
public partial class BackGroundList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<BackGroundResponse> Items => Parent == null ? new() : Parent.BackGrounds;
    string nameFilter;
    public List<BackGroundResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateBackGround/{Parent.Id}/{Parent.ProjectId}");

    }
    void Edit(BackGroundResponse response)
    {
        Navigation.NavigateTo($"/UpdateBackGround/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(BackGroundResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteBackGroundRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
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
