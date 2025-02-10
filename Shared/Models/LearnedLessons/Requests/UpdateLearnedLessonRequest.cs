using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.LearnedLessons.Requests
{
    public class UpdateLearnedLessonRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.LearnedLessons.EndPoint.Update;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.LearnedLessons.ClassName;
    }
}
