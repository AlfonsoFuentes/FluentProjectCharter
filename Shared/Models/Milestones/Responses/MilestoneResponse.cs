using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.FileResults.Generics.Reponses;
namespace Shared.Models.Milestones.Responses
{
    public class MilestoneResponse : BaseResponse, IUpdateStateResponse
    {
        public Guid ProjectId { get; set; }
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }




        public string EndPointName => StaticClass.Milestones.EndPoint.UpdateState;

        public string sStartDate => StartDate == null ? string.Empty : StartDate!.Value.ToString("d");
        public string sEndDate => EndDate == null ? string.Empty : EndDate!.Value.ToString("d");

     
        string _DurationInput = string.Empty;
        public string DurationInput
        {
            get => _DurationInput;
            set
            {
                if (TryParseDuration(value, out var parsedDuration))
                {
                    Duration = parsedDuration;
                    _DurationInput = value;
                }

            }
        }

        // Métodos para actualizar valores relacionados


        public DateTime? StartDate { get; set; } = null; // Nullable
        public DateTime? EndDate { get; set; } = null;   // Nullable
        public int Duration { get; set; } = 1;
        public string ParentTaskName { get; set; } = ""; // Nombre de la tarea principal (si es una subtarea)
        public string DependencyName { get; set; } = ""; // Nombre de la tarea de la que depende
  
        public TasksRelationTypeEnum DependencyType { get; set; } = TasksRelationTypeEnum.None; // Tipo de dependencia (SS, SE, ES, EE)
        public void UpdateEndDateAndDuration()
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                Duration = (int)(EndDate.Value - StartDate.Value).TotalDays;
                if (Duration < 0)
                {
                    Duration = 0;
                    EndDate = StartDate;
                }
            }
        }

        public void UpdateStartDateAndDuration()
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                Duration = (int)(EndDate.Value - StartDate.Value).TotalDays;
                if (Duration < 0)
                {
                    Duration = 0;
                    StartDate = EndDate;
                }
            }
        }

        public void UpdateEndDateFromDuration()
        {
            if (StartDate.HasValue)
            {
                EndDate = StartDate.Value.AddDays(Duration);
            }
        }
        public static bool TryParseDuration(string input, out int durationInDays)
        {
            durationInDays = 0;

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // Eliminar espacios en blanco
            input = input.Trim();

            // Extraer el número y la unidad usando una expresión regular
            var match = System.Text.RegularExpressions.Regex.Match(input, @"^(\d+)([dwmy]?)$");
            if (!match.Success)
            {
                return false; // Formato no válido
            }

            // Obtener el valor numérico
            if (!int.TryParse(match.Groups[1].Value, out var numericValue) || numericValue < 0)
            {
                return false; // Valor numérico no válido
            }

            // Obtener la unidad (d = día, w = semana, m = mes, y = año)
            var unit = match.Groups[2].Value.ToLower();
            switch (unit)
            {
                case "d": // Días
                    durationInDays = numericValue;
                    break;

                case "w": // Semanas
                    durationInDays = numericValue * 7;
                    break;

                case "m": // Meses (aproximadamente 30 días por mes)
                    durationInDays = numericValue * 30;
                    break;

                case "y": // Años (aproximadamente 365 días por año)
                    durationInDays = numericValue * 365;
                    break;

                case "": // Sin unidad, asumimos días
                    durationInDays = numericValue;
                    break;

                default:
                    return false; // Unidad no reconocida
            }

            return true;
        }
    }
}
