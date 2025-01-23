using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Scopes.Responses;
using static Shared.StaticClasses.StaticClass;

namespace FluentWeb.Pages.Deliverables;
#nullable disable
public partial class DeliverableList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }

    [Parameter]
    [EditorRequired]
    public ScopeResponse Parent { get; set; } = new();


        DeliverableResponse currentDeliverable =>
     App.Project == null || App.Project.CurrentCase == null
     || App.Project.CurrentCase.CurrentScope == null ? null :
         FilteredItems.FirstOrDefault(x => x.Id == App.Project.CurrentCase.CurrentScope.CurrentDeliverable!.Id);

    public List<DeliverableResponse> Items => Parent == null ? new() : Parent.Deliverables;
    string nameFilter;
    public List<DeliverableResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateDeliverable/{Parent.ProjectId}/{Parent.Id}");

    }

    void Edit(DeliverableResponse response)
    {
        Navigation.NavigateTo($"/UpdateDeliverable/{response.Id}/{Parent.ProjectId}");
    }

    async Task Up(DeliverableResponse response)
    {
        ChangeDeliverableOrderUpRequest request = new ChangeDeliverableOrderUpRequest()
        {
            Id = response.Id,
            ScopeId = response.ScopeId,
            ProjectId = response.ProjectId,

        };
        var result=await GenericService.Update(request);
        if(result.Succeeded)
        {
            await GetAll();
        }
    }
    async Task Down(DeliverableResponse response)
    {
        ChangeDeliverableOrderDowmRequest request = new()
        {
            Id = response.Id,
            ScopeId = response.ScopeId,
            ProjectId = response.ProjectId,
        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();
        }
    }
    public async Task Delete(DeliverableResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteDeliverableRequest request = new()
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
