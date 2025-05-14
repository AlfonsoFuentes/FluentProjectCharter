using Shared.Enums.TasksRelationTypes;
using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses
{

    public class NewTaskResponse : ValueObject, IResponse
    {
        public override string ToString()
        {
            return $"{WBS}-{Name}";
        }
        public Guid Id { get; set; } = Guid.Empty;

        public Guid ProjectId { get; set; }
        public Guid DeliverableId { get; set; }
        int _MainOrder;
        public int MainOrder
        {
            get { return _MainOrder; }
            set
            {
                _MainOrder = value;
                NotifyToObservers();
            }
        }
        public string ParentWBS { get; set; } = string.Empty;
        public int InternalOrder { get; set; }
        public string WBS => $"{ParentWBS}.{InternalOrder}";
        public Guid? TaskParentId { get; set; } // Referencia al padre (opcional)
        public List<NewTaskResponse> SubTasks { get; set; } = new(); // Colección de subtareas
        public List<NewTaskResponse> OrderedSubTasks => SubTasks.OrderBy(x => x.InternalOrder).ToList();
        public int LastInternalOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.Last().InternalOrder;
        public int LastMainOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.Last().MainOrder;
        public int FirstMainOrder => OrderedSubTasks.Count == 0 ? 0 : OrderedSubTasks.First().MainOrder;
        public TasksRelationTypeEnum DependencyType { get; set; } = TasksRelationTypeEnum.None;



        public bool HasDependency => Dependencies.Count > 0;
        public bool HasObservers => Observers.Count > 0;
        string? _DependencyList { get; set; }
        public string? DependencyList
        {
            get {  return _DependencyList; }
            set { _DependencyList = value; }
        }
        public double LagDays { get; set; }
        public double DurationDays { get; set; }
        public string? LagUnits { get; set; } = string.Empty;
        public string? DurationUnits { get; set; } = string.Empty;
        public string? Duration
        {
            get { return GetCalculatedDuration(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                SetDuration(value);
            }
        }
        public string? Lag
        {
            get { return GetLag(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                SetLag(value);
            }
        }
        public DateTime? PlannedStartDate
        {
            get { return GetPlannedStartDate(); }
            set
            {
                _PlannedStartDate = value;
            }
        }
        public DateTime? PlannedEndDate
        {
            get { return GetPlannedEndDate(); }
            set
            {
                _PlannedEndDate = value;
            }
        }


        DateTime? _PlannedStartDate = null;
        DateTime? GetPlannedStartDate()
        {


            if (OrderedSubTasks.Count > 0)
            {
                _PlannedStartDate = SubTasks.Min(x => x.PlannedStartDate);
            }
            else if (Dependencies.Count > 0)
            {
                switch (DependencyType.Id)
                {
                    case 0: //Finish Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya terminado.
                        DateTime? MaxDateFromDependencies = Dependencies.Max(x => x.PlannedEndDate);
                        _PlannedStartDate = MaxDateFromDependencies!.Value.AddDays(LagDays + 1);

                        break;
                    case 1://Start Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya comenzado
                        DateTime? MinDateFromDependencies = Dependencies.Min(x => x.PlannedStartDate);
                        _PlannedStartDate = MinDateFromDependencies!.Value.AddDays(LagDays);
                        break;

                    case 2: //Start Finish  La tarea dependiente debe terminar cuando la tarea precedente comience.
                        _PlannedStartDate = PlannedEndDate!.Value.AddDays(-DurationDays);
                        break;

                    case 3: //End End  La tarea dependiente debe terminar al mismo tiempo que la tarea precedente.
                        _PlannedStartDate = PlannedEndDate!.Value.AddDays(-DurationDays);
                        break;
                }


            }
            NotifyToObservers();
            return _PlannedStartDate;

        }
        DateTime? _PlannedEndDate = null;
        DateTime? GetPlannedEndDate()
        {

            if (OrderedSubTasks.Count > 0)
            {
                _PlannedEndDate = SubTasks.Max(x => x.PlannedEndDate);
            }
            else if (Dependencies.Count > 0)
            {
                switch (DependencyType.Id)
                {
                    case 0: //Finish Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya terminado.
                        _PlannedEndDate = PlannedStartDate!.Value.AddDays(DurationDays);
                        break;
                    case 1://Start Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya comenzado
                        _PlannedEndDate = PlannedStartDate!.Value.AddDays(DurationDays);

                        break;

                    case 2: //Start Finish  La tarea dependiente debe terminar cuando la tarea precedente comience.
                        DateTime? MinDateFromDependencies = Dependencies.Min(x => x.PlannedStartDate);
                        _PlannedEndDate = MinDateFromDependencies!.Value.AddDays(LagDays);
                        break;

                    case 3: //End End  La tarea dependiente debe terminar al mismo tiempo que la tarea precedente.
                        DateTime? MaxDateFromDependencies = Dependencies.Max(x => x.PlannedEndDate);
                        _PlannedEndDate = MaxDateFromDependencies!.Value.AddDays(LagDays);
                        break;
                }


            }
            NotifyToObservers();
            return _PlannedEndDate;
        }
        public void SetDuration(string? rawinput)
        {
            CalculatingDuration = true;

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
            DurationDays = 0;
            var newdurationunits = match.Groups[2].Value.ToLower();
            if (string.IsNullOrEmpty(newdurationunits))
            {
                newdurationunits = DurationUnits;
            }
            DurationUnits = newdurationunits;
            var newdurationdays = 0.0;
            switch (DurationUnits)
            {
                case "d":
                    newdurationdays = numericValue;
                    break;
                case "w":
                    newdurationdays = numericValue * 7;
                    break;
                case "m":
                    newdurationdays = numericValue * 30;
                    break;
                case "y":
                    newdurationdays = numericValue * 365;
                    break;
                case "":
                    newdurationdays = numericValue;

                    break;
                default:
                    newdurationdays = numericValue;

                    break;
            }
            DurationDays = newdurationdays;

        }

        public string GetCalculatedDuration()
        {
            CalculateDurationFromDates();
            return GetDuration();

        }
        public string GetDuration()
        {

            var result = 0.0;
            switch (DurationUnits)
            {
                case "d": result = DurationDays; break;
                case "w": result = Math.Round(DurationDays / 7, 1); break;
                case "m": result = Math.Round(DurationDays / 30, 1); break;
                case "y": result = Math.Round(DurationDays / 365, 1); break;
                default: result = DurationDays; break;
            }
            return $"{result}{DurationUnits}";
        }
        public void SetLag(string? rawinput)
        {

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
            LagDays = 0;
            var newlagunits = match.Groups[2].Value.ToLower();
            if (string.IsNullOrEmpty(newlagunits))
            {
                newlagunits = LagUnits;
            }
            LagUnits = newlagunits;
            double newlagdays = 0.0;
            switch (LagUnits)
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
            LagDays = newlagdays;

        }
        public string GetLag()
        {

            var result = 0.0;
            switch (LagUnits)
            {
                case "d": result = LagDays; break;
                case "w": result = Math.Round(LagDays / 7, 1); break;
                case "m": result = Math.Round(LagDays / 30, 1); break;
                case "y": result = Math.Round(LagDays / 365, 1); break;
                default: result = LagDays; break;
            }
            return $"{result}{LagUnits}";
        }
        bool CalculatingDuration = false;
        public void CalculateDurationFromDates()
        {
            if (!PlannedStartDate.HasValue || !PlannedEndDate.HasValue || CalculatingDuration)
            {
                return;
            }

            TimeSpan difference = PlannedEndDate.Value - PlannedStartDate.Value;

            DurationDays = difference.Days;


        }
        public void CalculateEndDate()
        {
            if (!PlannedStartDate.HasValue || DurationDays == 0)
            {
                return;
            }
          
            PlannedEndDate = _PlannedStartDate!.Value.AddDays(DurationDays);
            CalculatingDuration = false;
            NotifyToObservers();
        }
        //public void CalculateStartDate()
        //{
        //    if (!PlannedEndDate.HasValue || DurationDays == 0)
        //    {
        //        return;
        //    }
        //    _PlannedStartDate = _PlannedEndDate!.Value.AddDays(-DurationDays);
        //    NotifyToObservers();
        //}

        public void AddObserver(PublisherObserverResponse observador)
        {
            Observers.Add(observador);
        }
        public void AddDependency(PublisherObserverResponse observador)
        {
            Dependencies.Add(observador);
        }
        public void RemoveObserver(PublisherObserverResponse observador)
        {
            Observers.Remove(observador);
        }
        public void RemoveObservers(Predicate<PublisherObserverResponse> predicate)
        {
            Observers.RemoveAll(predicate);
        }
        public void RemoveDependency(PublisherObserverResponse observador)
        {
            Dependencies.Remove(observador);
        }
        public void RemoveAllObserver()
        {
            Observers.Clear();
        }
        public void RemoveAllDependencies()
        {
            Dependencies.Clear();
        }
        public List<PublisherObserverResponse> Observers { get; set; } = new List<PublisherObserverResponse>();
        public List<PublisherObserverResponse> Dependencies { get; set; } = new List<PublisherObserverResponse>();
       
        public void NotifyToObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update(MainOrder, _PlannedStartDate, _PlannedEndDate);
            }
        }
       
        public void SetObservers(List<PublisherObserverResponse> observers)
        {
            Observers = observers;
        }
        public List<PublisherObserverResponse> GetDependencies()
        {
            return Dependencies.Where(x => x.ObservedId == Id).ToList();
        }

    }
}
