using MudBlazor;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Responses;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Responses;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Responses;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Responses;
using Shared.Models.BudgetItems.IndividualItems.Instruments.Responses;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Pipes.Responses;
using Shared.Models.BudgetItems.IndividualItems.Structurals.Responses;
using Shared.Models.BudgetItems.IndividualItems.Taxs.Responses;
using Shared.Models.BudgetItems.IndividualItems.Testings.Responses;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;
using Shared.Models.BudgetItems.Requests;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.GanttTasks.Responses;
using Web.Pages.BudgetItems.Alterations;
using Web.Pages.BudgetItems.EHSs;
using Web.Pages.BudgetItems.Electricals;
using Web.Pages.BudgetItems.EngineeringDesigns;
using Web.Pages.BudgetItems.Equipments;
using Web.Pages.BudgetItems.Foundations;
using Web.Pages.BudgetItems.Instruments;
using Web.Pages.BudgetItems.Paintings;
using Web.Pages.BudgetItems.Pipings;
using Web.Pages.BudgetItems.Structurals;
using Web.Pages.BudgetItems.Taxs;
using Web.Pages.BudgetItems.Testings;
using Web.Pages.BudgetItems.Valves;
using Web.Templates;

namespace Web.Pages.BudgetItems;
public partial class BudgetItemsTableForGanttTasks
{
    public List<IBudgetItemResponse> Items => GanttTask.BudgetItems;

    [Parameter]
    public Func<Task> GetAll { get; set; } = null!;

    [Parameter]
    public GanttTaskResponse GanttTask { get; set; } = null!;


    string nameFilter = string.Empty;
    public List<IBudgetItemResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public async Task AddAlterations()
    {
        var parameters = new DialogParameters<AlterationDialog>
        {
             { x => x.Model, new AlterationResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<AlterationDialog>("Alteration", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddFoundations()
    {
        var parameters = new DialogParameters<FoundationDialog>
        {
             { x => x.Model, new FoundationResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<FoundationDialog>("Foundation", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddStructurals()
    {
        var parameters = new DialogParameters<StructuralDialog>
        {
             { x => x.Model, new StructuralResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<StructuralDialog>("Structural", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddEquipments()
    {
        var parameters = new DialogParameters<EquipmentDialog>
        {
             { x => x.Model, new EquipmentResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = await DialogService.ShowAsync<EquipmentDialog>("Equipment", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddValves()
    {
        var parameters = new DialogParameters<ValveDialog>
        {
             { x => x.Model, new ValveResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = await DialogService.ShowAsync<ValveDialog>("Valve", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddElectricals()
    {
        var parameters = new DialogParameters<ElectricalDialog>
        {
             { x => x.Model, new ElectricalResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<ElectricalDialog>("Electrical", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddPipes()
    {
        var parameters = new DialogParameters<PipeDialog>
        {
             { x => x.Model, new PipeResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<PipeDialog>("Pipe", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddInstruments()
    {
        var parameters = new DialogParameters<InstrumentDialog>
        {
             { x => x.Model, new InstrumentResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = await DialogService.ShowAsync<InstrumentDialog>("Instrument", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddEHSs()
    {
        var parameters = new DialogParameters<EHSDialog>
        {
             { x => x.Model, new EHSResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<EHSDialog>("EHS", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddPaintings()
    {
        var parameters = new DialogParameters<PaintingDialog>
        {
             { x => x.Model, new PaintingResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<PaintingDialog>("Painting", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddTaxs()
    {
        var parameters = new DialogParameters<TaxDialog>
        {
             { x => x.Model, new TaxResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<TaxDialog>("Tax", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddTestings()
    {
        var parameters = new DialogParameters<TestingDialog>
        {
             { x => x.Model, new TestingResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<TestingDialog>("Testing", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task AddEngineeringDesigns()
    {
        var parameters = new DialogParameters<EngineeringDesignDialog>
        {
             { x => x.Model, new EngineeringDesignResponse(){GanttTaskId=GanttTask.Id,ProjectId=GanttTask.ProjectId } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<EngineeringDesignDialog>("Engineering Design", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditAlterations(AlterationResponse model)
    {

        var parameters = new DialogParameters<AlterationDialog>
        {
             { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<AlterationDialog>("Alteration", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditFoundations(FoundationResponse model)
    {

        var parameters = new DialogParameters<FoundationDialog>
        {
             { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<FoundationDialog>("Foundation", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditStructurals(StructuralResponse model)
    {

        var parameters = new DialogParameters<StructuralDialog>
        {
           { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<StructuralDialog>("Structural", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditEquipments(EquipmentResponse model)
    {

        var parameters = new DialogParameters<EquipmentDialog>
        {
           { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = await DialogService.ShowAsync<EquipmentDialog>("Equipment", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditValves(ValveResponse model)
    {

        var parameters = new DialogParameters<ValveDialog>
        {
             { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = await DialogService.ShowAsync<ValveDialog>("Valve", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditElectricals(ElectricalResponse model)
    {

        var parameters = new DialogParameters<ElectricalDialog>
        {
            { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<ElectricalDialog>("Electrical", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditPipes(PipeResponse model)
    {

        var parameters = new DialogParameters<PipeDialog>
        {
          { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<PipeDialog>("Pipe", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditInstruments(InstrumentResponse model)
    {

        var parameters = new DialogParameters<InstrumentDialog>
        {
            { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = await DialogService.ShowAsync<InstrumentDialog>("Instrument", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditEHSs(EHSResponse model)
    {

        var parameters = new DialogParameters<EHSDialog>
        {
           { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<EHSDialog>("EHS", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditPaintings(PaintingResponse model)
    {

        var parameters = new DialogParameters<PaintingDialog>
        {
           { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<PaintingDialog>("Painting", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditTaxs(TaxResponse model)
    {

        var parameters = new DialogParameters<TaxDialog>
        {
          { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<TaxDialog>("Tax", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditTestings(TestingResponse model)
    {

        var parameters = new DialogParameters<TestingDialog>
        {
          { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<TestingDialog>("Testing", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    public async Task EditEngineeringDesigns(EngineeringDesignResponse model)
    {

        var parameters = new DialogParameters<EngineeringDesignDialog>
        {
             { x => x.Model, model },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<EngineeringDesignDialog>("Engineering Design", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }

    }
    async Task Edit(IBudgetItemResponse response)
    {
        switch (response)
        {
            case AlterationResponse alteration:
                await EditAlterations(alteration);
                break;
            case FoundationResponse foundation:
                await EditFoundations(foundation);
                break;
            case StructuralResponse structural:
                await EditStructurals(structural);
                break;
            case EquipmentResponse equipment:
                await EditEquipments(equipment);
                break;
            case ValveResponse valve:
                await EditValves(valve);
                break;
            case ElectricalResponse electrical:
                await EditElectricals(electrical);
                break;
            case PipeResponse pipe:
                await EditPipes(pipe);
                break;
            case InstrumentResponse instrument:
                await EditInstruments(instrument);
                break;
            case EHSResponse ehs:
                await EditEHSs(ehs);
                break;
            case PaintingResponse painting:
                await EditPaintings(painting);
                break;
            case TaxResponse tax:
                await EditTaxs(tax);
                break;
            case TestingResponse testing:
                await EditTestings(testing);
                break;
            case EngineeringDesignResponse engineeringDesign:
                await EditEngineeringDesigns(engineeringDesign);
                break;
        }

    }

    public async Task Delete(IBudgetItemResponse response)
    {
        var parameters = new DialogParameters<DialogTemplate>
        {
            { x => x.ContentText, $"Do you really want to delete {response.Name}? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<DialogTemplate>("Delete", parameters, options);
        var result = await dialog.Result;


        if (!result!.Canceled)
        {


            DeleteBudgetItemRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                GanttTaskId = GanttTask.Id,


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
    HashSet<IBudgetItemResponse> SelectedItems = null!;
    async Task DeleteGroup()
    {
        if (SelectedItems == null) return;
        var parameters = new DialogParameters<DialogTemplate>
        {
            { x => x.ContentText, $"Do you really want to delete this {SelectedItems.Count} Items? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<DialogTemplate>("Delete", parameters, options);
        var result = await dialog.Result;


        if (!result!.Canceled)
        {

            DeleteBudgetItemGroupRequest request = new()
            {
                DeleteGroup = SelectedItems.Select(x => x.Id).ToList(),
                ProjectId = GanttTask.ProjectId,
                GanttTaskId = GanttTask.Id,
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
}
