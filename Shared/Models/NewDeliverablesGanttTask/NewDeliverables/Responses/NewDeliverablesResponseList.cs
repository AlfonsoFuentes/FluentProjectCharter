using Shared.Enums.TasksRelationTypes;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Helpers;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses
{
    public class NewDeliverablesResponseList : BaseResponse, IMessageResponse, IResponseAll, IRequest
    {
        public string EndPointName => StaticClass.NewDeliverables.EndPoint.UpdateAll;
        public Guid ProjectId { get; set; }
        public string Legend => Name;
        public DateTime? InitialProjectStartDate { get; set; }
        public string ActionType => Id == Guid.Empty ? "created" : "updated";
        public string ClassName => StaticClass.NewDeliverables.ClassName;
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessage(Legend, ClassName, ActionType);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessage(Legend, ClassName, ActionType);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);
        public List<NewDeliverableResponse> Deliverables { get; set; } = new();
        public List<NewDeliverableResponse> OrderedDeliverables => Deliverables.OrderBy(x => x.InternalOrder).ToList();
        public List<NewTaskResponse> GanttTasks => Deliverables.Select(x => x.OrderedFlatDeliverableTasks).SelectMany(x => x).ToList();
        public List<NewTaskResponse> OrderedGanttTasks => GanttTasks.OrderBy(x => x.InternalOrder).ToList();
        public List<NewDeliverableGanttTaskRow> DeliverableFlatenList => FlatenList.Where(x => x.IsDeliverable == true).ToList();
        public List<NewDeliverableGanttTaskRow> TaskFlatenList => FlatenList.Where(x => x.IsTask == true).ToList();
        public List<NewDeliverableGanttTaskRow> FlatenList => Deliverables.FlattenList();
        public IEnumerable<NewDeliverableGanttTaskRow> OrderedFlatenList => FlatenList.OrderBy(x => x.MainOrder).ToList();
        public int LastMainOrder => OrderedFlatenList.LastOrDefault()?.MainOrder ?? 0;
        public int FirstMainOrder => OrderedFlatenList.FirstOrDefault()?.MainOrder ?? 0;
        public int LastInternalOrder => OrderedDeliverables.LastOrDefault()?.InternalOrder ?? 0;



        //Falta Calcular, validacion, Guardar tareas completas,actualizar GetAll, dependencias, Lag, Duration
    }
}
