using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.OrganizationStrategies.Records
{

    public class GetOrganizationStrategyByIdRequest : GetByIdMessageResponse, IGetById
    {

        public Guid Id { get; set; }
        public string EndPointName => StaticClass.OrganizationStrategys.EndPoint.GetById;
        public override string ClassName => StaticClass.OrganizationStrategys.ClassName;
    }
}
