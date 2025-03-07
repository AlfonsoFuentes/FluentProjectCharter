using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.GanttTasks.Responses
{
    public class DeliverableWithGanttTaskResponseListToUpdate : UpdateMessageResponse, IResponseAll,IRequest
    {
        public string ProjectName { get; set; } = string.Empty;
        public override string Legend => "GanttTasks";
        public override string ClassName => StaticClass.GanttTasks.ClassName;
        public Guid ProjectId { get; set; }
        public List<DeliverableWithGanttTaskResponseToUpdate> Deliverables { get; set; } = new();

        public string EndPointName => StaticClass.GanttTasks.EndPoint.UpdateEDT;
    }
}
