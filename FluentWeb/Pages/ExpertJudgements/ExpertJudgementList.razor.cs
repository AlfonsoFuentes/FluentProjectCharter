using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.ExpertJudgements.Requests;
using Shared.Models.ExpertJudgements.Responses;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.ExpertJudgements;
#nullable disable
public partial class ExpertJudgementList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    
    //[Inject]
    //private IStakeHolderService StakeHolderService { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<ExpertJudgementResponse> Items => Parent == null ? new() : Parent.ExpertJudgements;
    string nameFilter;
    public List<ExpertJudgementResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
  

    StakeHolderResponseList StakeHolderResponseList = new();
    //protected override async Task OnInitializedAsync()
    //{
    //    await UpdateStakeHolder();
    //}

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateExpertJudgement/{Parent.Id}/{Parent.ProjectId}");

    }



    void Edit(ExpertJudgementResponse response)
    {
        Navigation.NavigateTo($"/UpdateExpertJudgement/{response.Id}/{Parent.ProjectId}");
    }
   
    //async Task UpdateStakeHolder()
    //{
    //    var result = await StakeHolderService.GetAll();
    //    if (result.Succeeded)
    //    {
    //        StakeHolderResponseList = result.Data;
    //    }
    //}
    
    public async Task Delete(ExpertJudgementResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteExpertJudgementRequest request = new()
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
