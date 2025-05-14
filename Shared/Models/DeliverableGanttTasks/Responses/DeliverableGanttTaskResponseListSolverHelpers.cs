using System.Text;

namespace Shared.Models.DeliverableGanttTasks.Responses
{
    public static class DeliverableGanttTaskResponseListSolverHelpers
    {
        public static DateTime? GetCalculatedStartDate(this DeliverableGanttTaskResponse row)
        {
            DateTime? result = DateTime.MinValue;
            if (row.HasSubTask)
            {
                result = row.SubTasks.Min(x => x.StartDate);
            }
            else if (row.HasDependencies)
            {

                switch (row.DependencyType.Id)
                {
                    case 0: //Finish Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya terminado.
                        DateTime? MaxDateFromDependencies = row.Dependencies.Max(x => x.EndDate);
                        result = MaxDateFromDependencies!.Value.AddDays(row.LagInDays + 1);

                        break;
                    case 1://Start Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya comenzado
                        DateTime? MinDateFromDependencies = row.Dependencies.Min(x => x.StartDate);
                        result = MinDateFromDependencies!.Value.AddDays(row.LagInDays);
                        break;

                    case 2: //Start Finish  La tarea dependiente debe terminar cuando la tarea precedente comience.
                        result = row.EndDate!.Value.AddDays(-row.DurationInDays);
                        break;

                    case 3: //End End  La tarea dependiente debe terminar al mismo tiempo que la tarea precedente.
                        result = row.EndDate!.Value.AddDays(-row.DurationInDays);
                        break;
                }
            }
            return result;
        }
        public static DateTime? GetCalculateEndDate(this DeliverableGanttTaskResponse row)
        {
            DateTime? result = DateTime.MinValue;
            if (row.HasSubTask)
            {
                result = row.SubTasks.Max(x => x.EndDate);
            }
            else if (row.HasDependencies)
            {

                switch (row.DependencyType.Id)
                {
                    case 0: //Finish Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya terminado.
                        result = row.StartDate!.Value.AddDays(row.DurationInDays);
                        break;
                    case 1://Start Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya comenzado
                        result = row.StartDate!.Value.AddDays(row.DurationInDays);

                        break;

                    case 2: //Start Finish  La tarea dependiente debe terminar cuando la tarea precedente comience.
                        DateTime? MinDateFromDependencies = row.Dependencies.Min(x => x.StartDate);
                        result = MinDateFromDependencies!.Value.AddDays(row.LagInDays);
                        break;

                    case 3: //End End  La tarea dependiente debe terminar al mismo tiempo que la tarea precedente.
                        DateTime? MaxDateFromDependencies = row.Dependencies.Max(x => x.EndDate);
                        result = MaxDateFromDependencies!.Value.AddDays(row.LagInDays);
                        break;
                }
            }
            return result;
        }
        public static void CalculateDuration(this DeliverableGanttTaskResponse row)
        {
            if (row.StartDate.HasValue && row.EndDate.HasValue)
            {
                row.DurationInDays = (row.EndDate.Value - row.StartDate.Value).TotalDays;
            }
            else
            {
                row.DurationInDays = 0;
            }
        }
        public static void CalculateEndDate(this DeliverableGanttTaskResponse row)
        {
            if (row.StartDate.HasValue && row.DurationInDays > 0)
            {
                row.EndDate = row.StartDate?.AddDays(row.DurationInDays);
            }
            else
            {
                row.EndDate = null;
            }
        }
        public static string GetDuration(this DeliverableGanttTaskResponse row)
        {

            return $"{row.DurationInUnit}{row.DurationUnit}";
        }
        public static void SetDuration(this DeliverableGanttTaskResponse task, string? newduration)
        {
            var rawinput = newduration;
            if (string.IsNullOrWhiteSpace(rawinput))
            {
                rawinput = "1d";
            }
            var input = rawinput.Trim();

            var match = System.Text.RegularExpressions.Regex.Match(input, @"^(\d+)\s*([dwmy]?)$");
            if (!match.Success)
            {
                return;
            }
            if (!int.TryParse(match.Groups[1].Value, out var numericValue) || numericValue < 0)
            {
                return;
            }
            task.DurationInUnit = numericValue;
          
            var newdurationunits = match.Groups[2].Value.ToLower();
            if (string.IsNullOrEmpty(newdurationunits))
            {
                newdurationunits = task.DurationUnit;
            }
            task.DurationUnit = newdurationunits;
          
            switch (task.DurationUnit)
            {
                case "d":
                    task.DurationInDays = numericValue;
                    break;
                case "w":
                    task.DurationInDays = numericValue * 7;
                    break;
                case "m":
                    task.DurationInDays = numericValue * 30;
                    break;
                case "y":
                    task.DurationInDays = numericValue * 365;
                    break;
                case "":
                    task.DurationInDays = numericValue;

                    break;
                default:
                    task.DurationInDays = numericValue;

                    break;
            }
           

        }
        public static string SetDependeyList(this DeliverableGanttTaskResponseList response, DeliverableGanttTaskResponse row, string dependencylistFromUI)
        {

            row.Dependencies.Clear();
            row.DependencyList = string.Empty;
            if (string.IsNullOrEmpty(dependencylistFromUI))
                return string.Empty;
            var dependencyList = dependencylistFromUI.Split(',')
                                               .Select(dep => dep.Trim())
                                               .Where(dep => !string.IsNullOrEmpty(dep))
                                               .ToList();


            StringBuilder result = new StringBuilder();
            StringBuilder resultMessages = new StringBuilder();
            foreach (var dependency in dependencyList)
            {
                var publisher = response.GetRowFromMainOrder(dependency);
                if (publisher != null)
                {
                    var resultDependency = response.IsValidDependency(row, publisher);
                    if (!string.IsNullOrEmpty(resultDependency))
                    {
                        Console.WriteLine(resultDependency);
                        resultMessages.Append(resultDependency);
                        return resultMessages.ToString();
                    }
                    else
                    {
                        row.Dependencies.Add(publisher);

                        if (result.Length > 0)
                        {
                            result.Append(", ");
                        }
                        result.Append($"{dependency}");
                       
                    }
                }


            }
            row.DependencyList = result.ToString();
            return resultMessages.ToString();
        }
        public static string GetDependencyList(this DeliverableGanttTaskResponse row)
        {
            if (row.Dependencies.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder result = new StringBuilder();
            foreach (var dependency in row.Dependencies)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }
                result.Append($"{dependency.MainOrder}");
            }
            return result.ToString();
        }

        public static string GetLag(this DeliverableGanttTaskResponse row)
        {

            return $"{row.LagInUnits}{row.LagUnit}";
        }
        public static void SetLag(this DeliverableGanttTaskResponse task, string? newlag)
        {
            var rawinput = newlag;
            if (string.IsNullOrWhiteSpace(rawinput))
            {
                rawinput = "1d";
            }
            var input = rawinput.Trim();

            var match = System.Text.RegularExpressions.Regex.Match(input, @"^(\d+)\s*([dwmy]?)$");
            if (!match.Success)
            {
                return;
            }
            if (!int.TryParse(match.Groups[1].Value, out var numericValue) || numericValue < 0)
            {
                return;
            }
            task.LagInUnits = numericValue;
            task.LagInDays = 0;
            var newlagunits = match.Groups[2].Value.ToLower();
            if (string.IsNullOrEmpty(newlagunits))
            {
                newlagunits = task.LagUnit;
            }
            task.LagUnit = newlagunits;
            var newlagdays = 0.0;
            switch (task.LagUnit)
            {
                case "d":
                    newlagdays = numericValue;
                    break;
                case "w":
                    newlagdays = numericValue * 7;
                    break;
                case "m":
                    newlagdays = numericValue * 30;
                    break;
                case "y":
                    newlagdays = numericValue * 365;
                    break;
                case "":
                    newlagdays = numericValue;

                    break;
                default:
                    newlagdays = numericValue;

                    break;
            }
            task.LagInDays = newlagdays;

        }
    }
}
