using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.AcceptanceCriterias.Requests
{
    public class CreateAcceptanceCriteriaRequest : CreateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }

        public string Name { set; get; } = string.Empty;
        public string EndPointName => StaticClass.AcceptanceCriterias.EndPoint.Create;

        public override string Legend => Name;

        public override string ClassName => StaticClass.AcceptanceCriterias.ClassName;
   

    }
}
