using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Apps.Requests;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Projects.Mappers;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;

namespace FluentWeb.Pages;
#nullable disable
public partial class Home
{
    [CascadingParameter]
    public App App { get; set; }

   
    string nameFilter { get; set; } = string.Empty;
    Func<ProjectResponse, bool> fiterexpresion => x =>
       x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<ProjectResponse> FilteredItems => ProjectList.Items.Count == 0 ? new() : ProjectList.Items.Where(fiterexpresion).ToList();
   
   
    public ProjectResponseList ProjectList { get; set; } = new();
    bool DisableSaveButton(ProjectResponse model)
    {
        return string.IsNullOrEmpty(model.Name) ? true : false;

    }
    string CurrentRowName => SelectedRow == null ? string.Empty : Truncate(SelectedRow.Name, 30);
    private string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
    }
    ProjectResponse CreateRow { get; set; }
    ProjectResponse SelectedRow { get; set; }
    ProjectResponse EditRow { get; set; }
    ProjectResponse CurrentCompleteRow { get; set; }
    bool DisableUpButton => SelectedRow == null ? true : SelectedRow.Order == 1;
    bool DisableDownButton => SelectedRow == null ? true : SelectedRow.Order == ProjectList.LastOrder;
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
        pagination.TotalItemCountChanged += (sender, eventArgs) => StateHasChanged();
    }
    public async Task GetAll()
    {

        var result = await GenericService.GetAll<ProjectResponseList, ProjectGetAll>(new ProjectGetAll());
        if (result.Succeeded)
        {
            ProjectList = result.Data;
         
        }

    }
    public void AddNew()
    {
        CreateRow = new() { Id = Guid.NewGuid(), Name = "New MWO" };
        EditRow = null!;
        ProjectList.Items.Insert(0, CreateRow);
        StateHasChanged();
    }
    public async Task Create()
    {
        if (CreateRow == null) return;

        var result = await GenericService.Create(CreateRow.ToCreate());
        if (result.Succeeded)
        {
            CreateRow = null;
            await GetAll();

        }
    }
    void CancelCreate()
    {
        if (CreateRow == null) return;

        ProjectList.Items.Remove(CreateRow);
        CreateRow = null;
    }
    void CancelEdit()
    {
        if (EditRow == null) return;
        EditRow = null;
    }
    public void Edit(ProjectResponse response)
    {
       Navigation.NavigateTo($"/UpdateProject/{response.Id}");
    }
    public void Approve(ProjectResponse response)
    {
        Navigation.NavigateTo($"/ApproveProject/{response.Id}");
    }
    private void HandleRowClick(FluentDataGridRow<ProjectResponse> row)
    {
        SelectedRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;

        EditRow = EditRow == null ? null : SelectedRow == null ? null : EditRow.Id != SelectedRow.Id ? null : EditRow;


    }
    private void HandleRowDoubleClick(FluentDataGridRow<ProjectResponse> row)
    {
        EditRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;

        CancelCreate();
    }
    public async Task Delete(ProjectResponse response)
    {
      
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteProjectRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
            };
            var resultDelete = await GenericService.Delete(request);
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
    async Task Show(ProjectResponse response)
    {
    
        CurrentCompleteRow = response;
        CreateUpdateAppRequest model = new()
        {
            ProjectId = response.Id,
        };
        var result = await GenericService.Update(model);
        if (result.Succeeded)
        {
            await GetAll();

        }
    }
    async Task UnShow()
    {

        CurrentCompleteRow = null;
        CreateUpdateAppRequest model = new()
        {
            ProjectId = null,
        };
        var result = await GenericService.Update(model);
        if (result.Succeeded)
        {
            await GetAll();
        }
    }

    async Task Up()
    {
        if (SelectedRow == null) return;


        var result = await GenericService.Update(SelectedRow.ToUp());
        if (result.Succeeded)
        {
            await GetAll();
        }
    }
    async Task Down()
    {
        if (SelectedRow == null) return;

        var result = await GenericService.Update(SelectedRow.ToDown());

        if (result.Succeeded)
        {
            await GetAll();
        }
    }

    async Task Update(ProjectResponse model)
    {

        var result = await GenericService.Update(model.ToUpdateName());
        if (result.Succeeded)
        {

            await GetAll();
            EditRow = null!;
        }
    }

    //public async Task ExportProjectCharter(ProjectResponse response)
    //{

    //    var resultExport = await Service.ExportProjectCharter(response);
    //    if (resultExport.Succeeded)
    //    {

    //        Console.WriteLine(resultExport.Message);
    //        var downloadresult = await blazorDownloadFileService.DownloadFile(resultExport.Data.ExportFileName,
    //          resultExport.Data.Data, contentType: resultExport.Data.ContentType);
    //        if (downloadresult.Succeeded)
    //        {
    //            _snackBar.ShowSuccess($"Project Charter for {response.Name} exported succesfully");

    //        }
    //        else
    //        {
    //            _snackBar.ShowError($"Project Charter for {response.Name} not exported succesfully");
    //        }
    //    }
    //    else
    //    {
    //        _snackBar.ShowError($"Project Charter for {response.Name} not created succesfully");
    //    }
    //}
    //public async Task ExportProjectPlan(ProjectResponse response)
    //{

    //    var resultExport = await Service.ExportProjectPlann(response);
    //    if (resultExport.Succeeded)
    //    {

    //        Console.WriteLine(resultExport.Message);
    //        var downloadresult = await blazorDownloadFileService.DownloadFile(resultExport.Data.ExportFileName,
    //          resultExport.Data.Data, contentType: resultExport.Data.ContentType);
    //        if (downloadresult.Succeeded)
    //        {
    //            _snackBar.ShowSuccess($"Project Charter for {response.Name} exported succesfully");

    //        }
    //        else
    //        {
    //            _snackBar.ShowError($"Project Charter for {response.Name} not exported succesfully");
    //        }
    //    }
    //    else
    //    {
    //        _snackBar.ShowError($"Project Charter for {response.Name} not created succesfully");
    //    }
    //}

}
