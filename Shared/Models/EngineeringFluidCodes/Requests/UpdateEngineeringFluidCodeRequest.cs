using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.EngineeringFluidCodes.Requests
{
    public class UpdateEngineeringFluidCodeRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.EngineeringFluidCodes.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.EngineeringFluidCodes.ClassName;
        public string Code { get; set; } = string.Empty;
    }
}
