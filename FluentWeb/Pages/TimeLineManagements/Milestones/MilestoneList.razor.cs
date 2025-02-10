using Microsoft.AspNetCore.Components;
using Shared.Enums.MilestoneRelationTypes;
using Shared.Models.Milestones.Records;
using Shared.Models.Milestones.Requests;
using Shared.Models.Milestones.Responses;
using Shared.Models.Projects.Reponses;

namespace FluentWeb.Pages.TimeLineManagements.Milestones;
#nullable disable
public partial class MilestoneList
{
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public string IdType { get; set; }

    [Parameter]
    public Guid Id { get; set; }
    Guid? StartId;
    Guid? PlanningId;
    private void ValidateIdType()
    {
        if (IdType.Equals("Planning", StringComparison.OrdinalIgnoreCase))
        {
            PlanningId = Id;
        }
        else if (IdType.Equals("Start", StringComparison.OrdinalIgnoreCase))
        {
            StartId = Id;
        }

    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<MilestoneResponseList, MilestoneGetAll>(new MilestoneGetAll()
        {
            ProjectId = ProjectId,

        });
        if (result.Succeeded)
        {
            AllItems = StartId.HasValue ? result.Data.Items.Where(x => x.StartId == StartId).ToList()
                : PlanningId.HasValue ? result.Data.Items.Where(x => x.PlanningId == PlanningId).ToList() :
                result.Data.Items;
            LastOrder = result.Data.LastOrder;

            GetSelectedRowFromItems();
        }
    }
    void GetSelectedRowFromItems()
    {
        SelectedRow = SelectedRow == null ? null : AllItems.FirstOrDefault(x => x.Id == SelectedRow.Id);
    }
    protected override async Task OnInitializedAsync()
    {
        ValidateIdType();
        await GetAll();
    }

    public List<MilestoneResponse> AllItems { get; set; } = new();

    int LastOrder;

    MilestoneResponse SelectedRow = null!;
    bool DisableUpButton => SelectedRow == null ? true : SelectedRow.Order == 1;
    bool DisableDownButton => SelectedRow == null ? true : SelectedRow.Order == LastOrder;

    bool DisableRigthButton => SelectedRow == null ? true : SelectedRow.Order == 1 ? true : !string.IsNullOrEmpty(SelectedRow.ParentTaskName);
    bool DisableLeftButton => SelectedRow == null ? true : SelectedRow.Order == 1 ? true : string.IsNullOrEmpty(SelectedRow.ParentTaskName);
    bool DisableOthersButton => SelectedRow == null;
    void OnSelectRow(MilestoneResponse row)
    {
        if (SelectedRow == null)
        {
            SelectedRow = row;
        }
        else if (SelectedRow.Id == row.Id)
        {
            SelectedRow = null!;
        }
        else
        {
            SelectedRow = row;
        }

    }


    async Task Up()
    {
        if (SelectedRow == null) return;

        ChangeMilestoneOrderUpRequest request = new ChangeMilestoneOrderUpRequest()
        {
            Id = SelectedRow.Id,
            ProjectId = SelectedRow.ProjectId,


        };
        var result = await GenericService.Update(request);
        if (result.Succeeded)
        {
            await GetAll();
        }
    }
    async Task Down()
    {
        if (SelectedRow == null) return;
        ChangeMilestoneOrderDowmRequest request = new()
        {
            Id = SelectedRow.Id,
            ProjectId = SelectedRow.ProjectId,


        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();
        }
    }
    async Task Rigth()
    {
        if (SelectedRow == null) return;

        var items = AllItems.Where(x => x.ParentTaskName == string.Empty).ToList();
        var index = items.IndexOf(SelectedRow);

        var previousRow = items.ElementAt(index - 1);
        if (previousRow != null)
        {
            UpdateMilestoneRightRequest request = new UpdateMilestoneRightRequest()
            {
                Id = SelectedRow.Id,
                ParentId = previousRow!.Id,
                ProjectId = ProjectId,

                Name = SelectedRow.Name,

            };
            var result = await GenericService.Update(request);
            if (result.Succeeded)
            {
                
                SelectedRow.ParentTaskName = previousRow.Name;
                // Actualizar la tarea principal
                UpdateParentTask(previousRow);
                CalculateDependencies();
                await GetAll();
            }
            else
            {
                _snackBar.ShowError(result.Messages);
            }
        }


    }
    async Task Left()
    {
        if (SelectedRow == null) return;

        var index = AllItems.IndexOf(SelectedRow);


        UpdateMilestoneLeftRequest request = new UpdateMilestoneLeftRequest()
        {
            Id = SelectedRow.Id,

            ProjectId = ProjectId,

            Name = SelectedRow.Name,

        };
        var result = await GenericService.Update(request);
        if (result.Succeeded)
        {
    
            SelectedRow.ParentTaskName = "";
            // Actualizar la tarea principal
            CalculateDependencies();
            await GetAll();
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }

    }

    public async Task Delete()
    {
        if (SelectedRow == null) return;
        var dialog = await DialogService.ShowWarningAsync($"Delete {SelectedRow.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteMilestoneRequest request = new()
            {
                Id = SelectedRow.Id,
                Name = SelectedRow.Name,
                ProjectId = ProjectId,
            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll();
       


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

    private async Task AddTask()
    {
        CreateMilestoneRequest request = new CreateMilestoneRequest()
        {
            StartId = StartId,
            PlanningId = PlanningId,
            StartDate = DateTime.Now,
            Duration = "1d",
            EndDate = DateTime.Now,
            ProjectId = ProjectId,
            Name = $"New Milestone {AllItems.Count(t => string.IsNullOrEmpty(t.ParentTaskName)) + 1}"
        };
        var result = await GenericService.Create(request);
        if (result.Succeeded)
        {
   
            await GetAll();
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }


    }


    // Método para obtener las subtareas de una tarea principal
    private IEnumerable<MilestoneResponse> GetSubtasks(MilestoneResponse parentTask)
    {
        return AllItems.Where(t => t.ParentTaskName == parentTask.Name);
    }

    // Método para actualizar la fecha de inicio
    public async Task UpdateName(MilestoneResponse task, string newName)
    {
        task.Name = newName;
        //Aqui vamos a poner actualizar

        UpdateMilestoneNameRequest request = new()
        {
            Id = task.Id,
            Name = task.Name,

            ProjectId = ProjectId,
        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();
    
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }
    private async Task UpdateStartDate(MilestoneResponse task, DateTime? newValue)
    {
        task.StartDate = newValue;
        task.UpdateEndDateFromDuration();

        // Si es una subtarea, actualizar la tarea principal
        if (!string.IsNullOrEmpty(task.ParentTaskName))
        {
            var parentTask = AllItems.FirstOrDefault(t => t.Name == task.ParentTaskName);
            if (parentTask != null)
            {
                UpdateParentTask(parentTask);
            }
        }

        // Recalcular dependencias
        CalculateDependencies();
        //Aqui vamos a poner actualizar
        UpdateMilestoneAllDatesRequest request = new()
        {

            ProjectId = ProjectId,
            Items = AllItems,

        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();

        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }


    // Método para actualizar la fecha final
    private async Task UpdateEndDate(MilestoneResponse task, DateTime? newValue)
    {
        task.EndDate = newValue;
        task.UpdateStartDateAndDuration();

        // Si es una subtarea, actualizar la tarea principal
        if (!string.IsNullOrEmpty(task.ParentTaskName))
        {
            var parentTask = AllItems.FirstOrDefault(t => t.Name == task.ParentTaskName);
            if (parentTask != null)
            {
                UpdateParentTask(parentTask);
            }
        }

        // Recalcular dependencias
        CalculateDependencies();
        //Aqui vamos a poner actualizar
        UpdateMilestoneAllDatesRequest request = new()
        {

            ProjectId = ProjectId,
            Items = AllItems,

        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();
        
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }

    // Método para actualizar la duración
    private async Task UpdateDuration(MilestoneResponse task, string newValue)
    {
        if (MilestoneResponse.TryParseDuration(newValue, out var parsedDuration))
        {
            task.DurationInput = newValue;

            // Actualizar la fecha final basada en la nueva duración
            task.UpdateEndDateFromDuration();

            // Si es una subtarea, actualizar la tarea principal
            if (!string.IsNullOrEmpty(task.ParentTaskName))
            {
                var parentTask = AllItems.FirstOrDefault(t => t.Name == task.ParentTaskName);
                if (parentTask != null)
                {
                    UpdateParentTask(parentTask);
                }
            }

            // Recalcular dependencias
            CalculateDependencies();
        }
        //Aqui vamos a poner actualizar
        UpdateMilestoneAllDatesRequest request = new()
        {

            ProjectId = ProjectId,
            Items = AllItems,

        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();

        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }


    // Método para actualizar el nombre de la dependencia
    private async Task UpdateDependency(MilestoneResponse task, MilestoneResponse dependency)
    {
        var dependencyModel = dependency;
        task.DependencyName = dependencyModel == null ? string.Empty : dependencyModel.Name;

        CalculateDependencies();
        //Aqui vamos a poner actualizar

        UpdateMilestoneDependencyRequest request = new()
        {

            ProjectId = ProjectId,
            Items = AllItems,

            DependencyId = dependencyModel == null ? null : dependencyModel.Id,
            Id = task.Id,
            DependencyType = MilestoneRelationTypeEnum.FinishStart.Name,


        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();
    
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }

    // Método para actualizar el tipo de dependencia
    private async Task UpdateDependencyType(MilestoneResponse task, MilestoneRelationTypeEnum type)
    {


        task.DependencyType = type;
        CalculateDependencies();
        //Aqui vamos a poner actualizar
        UpdateMilestoneDependencyTypeRequest request = new()
        {

            ProjectId = ProjectId,
            Items = AllItems,

            Id = task.Id,
            DependencyType = task.DependencyType.Name,


        };
        var result = await GenericService.Update(request);

        if (result.Succeeded)
        {
            await GetAll();
       
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }

    // Método para calcular fechas basadas en dependencias
    private void CalculateDependencies()
    {
        foreach (var task in AllItems)
        {
            if (!string.IsNullOrEmpty(task.DependencyName))
            {
                var dependencyTask = AllItems.FirstOrDefault(t => t.Name == task.DependencyName);
                if (dependencyTask != null)
                {
                    switch (task.DependencyType.Id)
                    {
                        case 0: // Start-Start
                            task.StartDate = dependencyTask.StartDate;

                            task.UpdateEndDateFromDuration();
                            break;

                        case 1: // Start-End
                            task.EndDate = dependencyTask.StartDate;
                            task.StartDate = task.EndDate!.Value.AddDays(-task.Duration);
                            task.UpdateStartDateAndDuration();
                            break;

                        case 2: // End-Start
                            task.StartDate = dependencyTask.EndDate;

                            task.UpdateEndDateFromDuration();
                            break;

                        case 3: // End-End
                            task.EndDate = dependencyTask.EndDate;
                            task.StartDate = task.EndDate!.Value.AddDays(-task.Duration);
                            task.UpdateStartDateAndDuration();
                            break;
                    }
                }
            }
        }
    }

    // Método para actualizar la tarea principal basada en sus subtareas
    private void UpdateParentTask(MilestoneResponse parentTask)
    {
        var subtasks = GetSubtasks(parentTask);

        if (subtasks.Any())
        {
            // Calcular la fecha de inicio más temprana
            parentTask.StartDate = subtasks.Min(st => st.StartDate);

            // Calcular la fecha final más tardía
            parentTask.EndDate = subtasks.Max(st => st.EndDate);

            // Calcular la duración
            parentTask.UpdateEndDateAndDuration();
        }
        else
        {
            // Si no hay subtareas, resetear las fechas y duración
            parentTask.StartDate = null;
            parentTask.EndDate = null;
            parentTask.Duration = 0;
        }

        // Recalcular dependencias
        CalculateDependencies();
    }

    // Método para obtener las tareas que dependen de una tarea específica
    private IEnumerable<MilestoneResponse> GetDependentTasks(MilestoneResponse task)
    {
        return AllItems.Where(t => t.DependencyName == task.Name);
    }
}
