using Blazored.FluentValidation;
using MudBlazor;
using Shared.Enums.TasksRelationTypes;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Helpers;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Records;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses;
using Shared.Models.Projects.Reponses;
using System.ComponentModel.DataAnnotations;
using Web.Templates;

namespace Web.Pages.ProjectDependant.NewDeliverableWithGanttTasks;
public partial class NewDeliverablesWithGanttTask
{
    NewDeliverablesResponseList Response { get; set; } = new();
    [Parameter]
    public ProjectResponse Project { get; set; } = null!;
    NewDeliverableGanttTaskRow SelectedRow = null!;
    NewDeliverableGanttTaskRow EditRow = null!;
    int PageSize => Response.FlatenList.Count < 10 ? 10 : Response.FlatenList.Count;
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        if (Project == null) return;

        var result = await GenericService.GetAll<NewDeliverablesResponseList, GetAllNewDeliverables>(new GetAllNewDeliverables
        {
            ProjectId = Project.Id,
        });
        if (result.Succeeded)
        {
            Response = result.Data;

        }


    }
    public void AddDeliverable()
    {
        EditRow = Response.CreateDeliverable()!;



    }

    void AddSubTask()
    {
        if (SelectedRow == null) return;
        EditRow = Response.CreateTask(SelectedRow)!;


    }

    async Task Delete(NewDeliverableGanttTaskRow selected)
    {
        if (selected == null) return;
        var parameters = new DialogParameters<DialogTemplate>
        {
            { x => x.ContentText, $"Do you really want to delete {selected.Name}? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<DialogTemplate>("Delete", parameters, options);
        var result = await dialog.Result;


        if (!result!.Canceled)
        {
            Response.Delete(selected);
            Response.Calculate();
            var resultRecalculate = await GenericService.Post(Response);
            if (resultRecalculate.Succeeded)
            {
                await GetAll();
                _snackBar.ShowSuccess(resultRecalculate.Messages);
            }
            else
            {
                _snackBar.ShowError(resultRecalculate.Messages);
            }


        }

    }

    void Cancel(NewDeliverableGanttTaskRow selectedRow)
    {
        if (selectedRow.IsCreating)
            Response.CancelCreate(selectedRow);
        else if (EditRow != null)
        {
            //Roll Back changes
        }
        Response.Calculate();
        //else if (backupEdit != null)
        //{
        //    selectedRow.Deliverable = backupEdit.Deliverable;
        //    selectedRow.Task = backupEdit.Task; 

        //}
        EditRow = null!;

        StateHasChanged();
    }
    async Task Save()
    {
        if(EditRow == null) return;
        IResult result = null!;

        Response.Calculate();
        result = await GenericService.Post(Response);

        if (result.Succeeded)
        {
            await GetAll();

            _snackBar.ShowSuccess(result.Messages);
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
        EditRow = null!;



    }
    void Edit(NewDeliverableGanttTaskRow selectedRow)
    {
        if (selectedRow == null) return;
        EditRow = selectedRow;

        //backupEdit = new()
        //{
        //    Deliverable = selectedRow.Deliverable,
        //    Task = selectedRow.Task,


        //};



    }
    NewDeliverableGanttTaskRow backupEdit = null!;


    void OnSelectRow(NewDeliverableGanttTaskRow selectedRow)
    {
        if (EditRow != null)
        {
            return;
        }
        SelectedRow = selectedRow;
        EditRow = null!;

        StateHasChanged();
    }

    string SelectedRowName => SelectedRow == null ? string.Empty : SelectedRow.Name;

    async Task MoveDown()
    {
        if (SelectedRow == null) return;
        if (Response.MoveDown(SelectedRow))
        {
            Response.Calculate();
            var result = await GenericService.Post(Response);

            if (result.Succeeded)
            {
                _snackBar.ShowSuccess(result.Messages);
            }
            else
            {
                _snackBar.ShowError(result.Messages);
            }
        }

    }

    async Task MoveUp()
    {
        if (SelectedRow == null) return;

        if (Response.MoveUp(SelectedRow))
        {
            Response.Calculate();
            var result = await GenericService.Post(Response);

            if (result.Succeeded)
            {
                _snackBar.ShowSuccess(result.Messages);
            }
            else
            {
                _snackBar.ShowError(result.Messages);
            }
        }




    }
    async Task MoveRight()
    {
        if (SelectedRow == null) return;
        if (Response.MoveRight(SelectedRow))
        {
            Response.Calculate();
            var result = await GenericService.Post(Response);
            if (result.Succeeded)
            {
                _snackBar.ShowSuccess(result.Messages);
            }
            else
            {
                _snackBar.ShowError(result.Messages);
            }

        }


    }
    async Task MoveLeft()
    {
        if (SelectedRow == null) return;
        if (Response.MoveLeft(SelectedRow))
        {
            Response.Calculate();
            var result = await GenericService.Post(Response);
            if (result.Succeeded)
            {
                _snackBar.ShowSuccess(result.Messages);
            }
            else
            {
                _snackBar.ShowError(result.Messages);
            }
        }



    }
    bool DisableButtonCanMoveDown()
    {
        if (SelectedRow == null) return true;

        return Response.DisableButtonCanMoveDown(SelectedRow);

    }
    bool DisableButtonCanMoveUp()
    {
        if (SelectedRow == null) return true;
        return Response.DisableButtonCanMoveUp(SelectedRow);
    }
    bool DisableButtonCanMoveLeft()
    {
        if (SelectedRow == null) return true;
        return Response.DisableButtonCanMoveLeft(SelectedRow);
    }
    bool DisableButtonCanMoveRight()
    {
        if (SelectedRow == null) return true;
        return Response.DisableButtonCanMoveRight(SelectedRow);
    }

    void OnChangeStartDate()
    {
        if (EditRow == null) return;
        Response.SetPlannedStartDate(EditRow/*, EditRow.PlannedStartDate*/);
        StateHasChanged();
    }
    void OnChangeEndDate()
    {
        if (EditRow == null) return;
        Response.SetPlannedEndDate(EditRow/*, EditRow.PlannedEndDate*/);
        StateHasChanged();
    }
    void OnChangeDuration(/*string? newDuration*/)
    {
        if (EditRow == null) return;
        

        Response.SetDuration(EditRow/*, newDuration*/);
        StateHasChanged();
    }
    void OnChangeDependencies(/*string? newDependencies*/)
    {
        if (EditRow == null) return;
       
        var currenDependencies = EditRow.Task.DependencyList;
        if (string.IsNullOrEmpty(currenDependencies))
        {
            EditRow.Task.RemoveAllDependencies();
            Response.RemoveObserver(EditRow);
            return;
        }
        var lasdependency = currenDependencies.Last();
        if (lasdependency == ',')
        {
            return;
        }
        Response.RemoveObserver(EditRow);
        var resultMessages = Response.SetDependencyList(EditRow/*, newDependencies*/);
        if (!string.IsNullOrEmpty(resultMessages))
        {
            _snackBar.ShowError(resultMessages);
            EditRow.DependencyList = string.Empty;
        }

        StateHasChanged();
    }
    void OnChangeLag(/*string? newLag*/)
    {
        if (EditRow == null) return;
        Response.SetLag(EditRow/*, newLag*/);
        StateHasChanged();
    }
    void OnChangeDependencyType(/*TasksRelationTypeEnum newType*/)
    {
        if (EditRow == null) return;
        Response.SetDependencyType(EditRow/*, newType*/);
        StateHasChanged();
    }
    private bool Validated { get; set; } = true;
    // Método asincrónico para realizar la validación
    public async Task ValidateAsync()
    {
        Validated = true;// _fluentValidationValidator == null ? false : await _fluentValidationValidator.ValidateAsync(options => { options.IncludeAllRuleSets(); });
    }


    FluentValidationValidator _fluentValidationValidator = null!;
}
