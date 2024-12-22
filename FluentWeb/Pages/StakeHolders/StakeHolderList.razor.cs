using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Projects.Reponses;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.Generic;
using Web.Infrastructure.Managers.StakeHolders;

namespace FluentWeb.Pages.StakeHolders;
#nullable disable
public partial class StakeHolderList
{
    [CascadingParameter]
    public App App { get; set; }
    [Inject]
    private IGenericService Service {  get; set; }

    [Inject]
    private IStakeHolderService StakeHolderService { get; set; } = null!;
    public List<StakeHolderResponse> Items => Reponse.Items;
    string nameFilter;
    public List<StakeHolderResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    StakeHolderResponseList Reponse = new();
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        var result = await StakeHolderService.GetAll();
        if (result.Succeeded)
        {
            Reponse = result.Data;
        }
    }
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateStakeHolder");

    }


    void Edit(StakeHolderResponse response)
    {
        Navigation.NavigateTo($"/UpdateStakeHolder/{response.Id}");
    }
    public async Task Delete(StakeHolderResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteStakeHolderRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,

            };
            var resultDelete = await Service.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
