using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Constrainsts.Requests
{
    public class UpdateConstrainstRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DeliverableId { get; set; }
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Constrainsts.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Constrainsts.ClassName;
    }
}
