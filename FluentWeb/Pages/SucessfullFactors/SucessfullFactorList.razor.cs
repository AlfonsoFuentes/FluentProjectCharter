using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.SucessfullFactors.Requests;
using Shared.Models.SucessfullFactors.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.SucessfullFactors;
#nullable disable
public partial class SucessfullFactorList
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
    public List<SucessfullFactorResponse> Items => Parent == null ? new() : Parent.SucessfullFactors;
    string nameFilter;
    public List<SucessfullFactorResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateSucessfullFactor/{Parent.Id}/{Parent.ProjectId}");

    }

    void CancelAsync()
    {

    }

    void Edit(SucessfullFactorResponse response)
    {
        Navigation.NavigateTo($"/UpdateSucessfullFactor/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(SucessfullFactorResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteSucessfullFactorRequest request = new()
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
