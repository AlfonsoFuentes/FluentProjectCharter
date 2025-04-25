using MudBlazor;
using Shared.Models.BudgetItems.Mappers;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.Projects.Reponses;
using Shared.Models.PurchaseOrders.Responses;
using Web.Pages.PurchaseOrders.Dialogs;

namespace Web.Pages.BudgetItems;
public partial class BudgetItemsWithPurchaseOrders
{
    [Parameter]
    public ProjectResponse Project { get; set; } = new();

    BudgetItemWithPurchaseOrderResponseList ResponseList = new();
    public List<BudgetItemWithPurchaseOrdersResponse> Items => ResponseList.Items;
    //   public string NameFilter { get; set; } = string.Empty;

    //   public Func<PurchaseOrderResponse, bool> CriteriaPurchaseorders => x => x.
    //  SupplierName.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //x.SupplierNickName.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //x.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //x.PONumber.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //x.PurchaseRequisition.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //x.CostCenter.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //x.PurchaseOrderStatus.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase);
    //   public Func<BudgetItemWithPurchaseOrdersResponse, bool> Criteria => x =>
    //   x.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //   x.Tag.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //   x.Nomenclatore.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase) ||
    //   x.PurchaseOrders.Any(x => x.SupplierName.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase)) ||
    //   x.PurchaseOrders.Any(x => x.SupplierNickName.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase)) ||
    //   x.PurchaseOrders.Any(x => x.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase)) ||
    //   x.PurchaseOrders.Any(x => x.PONumber.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase)) ||
    //   x.PurchaseOrders.Any(x => x.PurchaseRequisition.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase)) ||
    //   x.PurchaseOrders.Any(x => x.CostCenter.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase)) ||
    //   x.PurchaseOrders.Any(x => x.PurchaseOrderStatus.Name.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase));
    //   public List<BudgetItemWithPurchaseOrdersResponse> FilteredItems => GetFilteredItems();

    //   List<BudgetItemWithPurchaseOrdersResponse> GetFilteredItems()
    //   {



    //       if (string.IsNullOrEmpty(NameFilter)) return Items;

    //       var filteredItems = Items.Where(Criteria).ToList();
    //       List<BudgetItemWithPurchaseOrdersResponse> result = new();
    //       foreach (var item in filteredItems)
    //       {
    //           var pos = item.PurchaseOrders.Where(CriteriaPurchaseorders);
    //           if (pos.Any())
    //           {
    //               item.PurchaseOrders = pos.Select(x => x).ToList();
    //               result.Add(item);
    //           }

    //       }

    //       return result;
    //   }
    public string NameFilter { get; set; } = string.Empty;

    // Criterio para filtrar PurchaseOrders
    private Func<PurchaseOrderResponse, bool> CriteriaPurchaseorders => x =>
        ContainsFilter(x.SupplierName) ||
        ContainsFilter(x.SupplierNickName) ||
        ContainsFilter(x.Name) ||
        ContainsFilter(x.PONumber) ||
        ContainsFilter(x.PurchaseRequisition) ||
        ContainsFilter(x.CostCenter.Name) ||
        ContainsFilter(x.PurchaseOrderStatus.Name);

    // Criterio para filtrar BudgetItems
    private Func<BudgetItemWithPurchaseOrdersResponse, bool> Criteria => x =>
        ContainsFilter(x.Name) ||
        ContainsFilter(x.Tag) ||
        ContainsFilter(x.Nomenclatore) ||
        x.PurchaseOrders.Any(CriteriaPurchaseorders);

    // Propiedad para obtener los elementos filtrados
    public List<BudgetItemWithPurchaseOrdersResponse> FilteredItems = new();

    // Método auxiliar para verificar si un valor contiene el filtro (ignorando mayúsculas/minúsculas)
    private bool ContainsFilter(string value) =>
        !string.IsNullOrEmpty(value) && value.Contains(NameFilter, StringComparison.InvariantCultureIgnoreCase);

    // Método principal para obtener los elementos filtrados
    void OnNameFilterChanged()
    {
        // Llamar a StateHasChanged para actualizar la vista
        FilteredItems = GetFilteredItems();
        StateHasChanged();
    }
    private List<BudgetItemWithPurchaseOrdersResponse> GetFilteredItems()
    {
        if (string.IsNullOrEmpty(NameFilter))
        {
            // Si no hay filtro, devolver todos los elementos sin modificar
            return Items;
        }

        // Filtrar los BudgetItems que cumplen con el criterio general
        var filteredItems = Items.Where(Criteria).ToList();

        // Crear una lista para almacenar los resultados finales
        var result = new List<BudgetItemWithPurchaseOrdersResponse>();

        foreach (var item in filteredItems)
        {
            // Filtrar las PurchaseOrders dentro de cada BudgetItem
            var filteredPurchaseOrders = item.PurchaseOrders.Where(CriteriaPurchaseorders).ToList();

            // Determinar si el BudgetItem debe ser incluido en los resultados
            if (filteredPurchaseOrders.Any() || ContainsFilter(item.Name) || ContainsFilter(item.Tag) || ContainsFilter(item.Nomenclatore))
            {
                // Crear una nueva instancia del BudgetItem para evitar modificar los objetos originales
                var newItem = item.Map();
                newItem.PurchaseOrders = filteredPurchaseOrders.Any()
                        ? filteredPurchaseOrders // Incluir solo las PurchaseOrders filtradas
                        : item.PurchaseOrders; // Incluir todas las PurchaseOrders si el BudgetItem hace match directamente

                result.Add(newItem);
            }
        }

        return result;
    }
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<BudgetItemWithPurchaseOrderResponseList, BudgetItemWithPurchaseOrderGetAll>(new BudgetItemWithPurchaseOrderGetAll()
        {
            ProjectId = Project.Id
        });
        if (result.Succeeded)
        {
            ResponseList = result.Data;
            OnNameFilterChanged();
        }
    }
    async Task AddPurchaseorder(BudgetItemWithPurchaseOrdersResponse response)
    {
        var parameters = new DialogParameters<CreatePurchaseOrderDialog>
        {
            { x => x.BudgetItem, response},
            { x => x.ResponseList, ResponseList },


        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraLarge };

        var dialog = await DialogService.ShowAsync<CreatePurchaseOrderDialog>("Create Purchase Order", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }
    }

    [Parameter]
    public EventCallback ExportExcel { get; set; }
    [Parameter]
    public EventCallback ExportPDF { get; set; }
}
