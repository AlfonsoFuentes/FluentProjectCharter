using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.LearnedLessons.Requests
{
    public class CreateLearnedLessonRequest : CreateMessageResponse, IRequest
    {
        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
       
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndPointName => StaticClass.LearnedLessons.EndPoint.Create;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.LearnedLessons.ClassName;
    }
}
