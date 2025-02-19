using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.FileResults.Generics.Reponses;
namespace Shared.Models.Deliverables.Responses
{
    //public class DeliverableResponse2 : BaseResponse, IUpdateStateResponse
    //{
    //    public override string ToString()
    //    {
    //        return Name;
    //    }

    //    public Guid ProjectId { get; set; }
    //    public Guid? StartId { get; set; }
    //    public Guid? PlanningId { get; set; }
    //    public Guid? DependantId { get; set; }
    //    public int LabelOrder { get; set; }
    //    public string WBS { get; set; } = string.Empty;
    //    public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateState;
    //    public Guid? ParentDeliverableId { get; set; } // Referencia al padre (opcional)
    //    public List<DeliverableResponse2> SubDeliverables { get; set; } = new();
    //    public List<DeliverableResponse2> OrderedSubDeliverables => SubDeliverables.Count == 0 ? new() : SubDeliverables.OrderBy(x => x.LabelOrder).ToList();
    //    public List<DeliverableResponse2> Dependants { get; set; } = new(); // Colección de subtareas

    //    public TasksRelationTypeEnum DependencyType { get; set; } = TasksRelationTypeEnum.None;


    //    public void RemoveSubDeliverable(DeliverableResponse2 subDeliverable)
    //    {

    //        if (SubDeliverables.Contains(subDeliverable))
    //        {
    //            subDeliverable.ParentDeliverableId = null;
    //            SubDeliverables.Remove(subDeliverable);
    //        }

    //    }

    //    public DateTime? StartDate { get; set; }
    //    public DateTime? EndDate { get; set; }

    //    int Duration { get; set; } = 1;
    //    public string sDependences => string.Join(",", Dependants.OrderBy(x => x.LabelOrder).Select(d => d.LabelOrder.ToString()));
    //    private string? _DurationInput = string.Empty;
    //    public string? DurationInput
    //    {
    //        get => GetDurationInput();
    //        set
    //        {
    //            if (TryParseDuration(value, out var parsedDuration))
    //            {
    //                Duration = parsedDuration;

    //            }
    //            _DurationInput = value;
    //        }
    //    }

    //    public static bool TryParseDuration(string? input, out int durationInDays)
    //    {
    //        durationInDays = 0;
    //        string unitduration = "d";
    //        if (string.IsNullOrWhiteSpace(input))
    //        {
    //            return false;
    //        }
    //        input = input.Trim();
    //        var match = System.Text.RegularExpressions.Regex.Match(input, @"^(\d+)\s*([dwmy]?)$");
    //        if (!match.Success)
    //        {
    //            return false;
    //        }
    //        if (!int.TryParse(match.Groups[1].Value, out var numericValue) || numericValue < 0)
    //        {
    //            return false;
    //        }
    //        unitduration = match.Groups[2].Value.ToLower();

    //        switch (unitduration)
    //        {
    //            case "d":
    //                durationInDays = numericValue;
    //                break;
    //            case "w":
    //                durationInDays = numericValue * 7;
    //                break;
    //            case "m":
    //                durationInDays = numericValue * 30;
    //                break;
    //            case "y":
    //                durationInDays = numericValue * 365;
    //                break;
    //            case "":
    //                durationInDays = numericValue;
    //                break;
    //            default:
    //                return false;
    //        }
    //        return true;
    //    }

    //    private string GetDurationInput()
    //    {
    //        var duration = CalculateDuration();
    //        var unit = ExtractUnitFromDurationInput(_DurationInput);
    //        var result = $"{duration}{unit}";
    //        return result;
    //    }

    //    // Método privado para calcular la duración
    //    private int CalculateDuration()
    //    {
    //        if (!StartDate.HasValue || !EndDate.HasValue)
    //        {
    //            return 0; // No se puede calcular sin fechas válidas
    //        }

    //        // Calcular la diferencia en días entre StartDate y EndDate
    //        var durationInDays = (int)(EndDate.Value - StartDate.Value).TotalDays + 1;

    //        // Extraer la unidad de medida del DurationInput
    //        var unit = ExtractUnitFromDurationInput(_DurationInput);

    //        // Convertir la duración en días a la unidad especificada
    //        return unit switch
    //        {
    //            "d" => durationInDays, // Días
    //            "w" => durationInDays / 7, // Semanas
    //            "m" => (int)(durationInDays / 30.44), // Meses (promedio de 30.44 días por mes)
    //            "y" => (int)(durationInDays / 365.25), // Años (promedio de 365.25 días por año)
    //            _ => durationInDays // Por defecto, usar días
    //        };
    //    }

    //    // Método para extraer la unidad de medida del DurationInput
    //    private string ExtractUnitFromDurationInput(string? durationInput)
    //    {
    //        if (string.IsNullOrEmpty(durationInput))
    //        {
    //            return "d"; // Unidad predeterminada: días
    //        }

    //        // Extraer el último carácter como la unidad
    //        var lastChar = durationInput[^1];
    //        return char.IsLetter(lastChar) ? lastChar.ToString().ToLower() : "d";
    //    }

    //    public static DeliverableResponse2? FindParent(DeliverableResponse2 child, IEnumerable<DeliverableResponse2> deliverables)
    //    {
    //        foreach (var deliverable in deliverables)
    //        {
    //            if (deliverable.SubDeliverables.Contains(child))
    //            {
    //                return deliverable;
    //            }

    //            var parent = FindParent(child, deliverable.SubDeliverables);
    //            if (parent != null)
    //            {
    //                return parent;
    //            }
    //        }

    //        return null;
    //    }

    //    public void UpdateStartDate(DateTime? startdate)
    //    {
    //        StartDate = startdate;
    //        if (!Dependants.Any() && StartDate.HasValue)
    //        {
    //            EndDate = StartDate.Value.AddDays(Duration);
    //        }
    //    }
    //    public void UpdateEndDate(DateTime? endate)
    //    {
    //        EndDate = endate;
    //        if (!Dependants.Any() && EndDate.HasValue)
    //        {
    //            StartDate = EndDate.Value.AddDays(-Duration);
    //        }
    //    }
    //    public void UpdateDuration(string duration)
    //    {
    //        DurationInput = duration;
    //        if (!Dependants.Any() )
    //        {
    //            if(StartDate.HasValue)
    //            {
    //                EndDate = StartDate.Value.AddDays(Duration);
    //            }
    //            else if(EndDate.HasValue)
    //            {
    //                StartDate = EndDate.Value.AddDays(-Duration);
    //            }
    //        }
            
    //    }


    //}
}
