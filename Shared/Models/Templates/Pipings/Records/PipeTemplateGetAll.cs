using Shared.Models.FileResults.Generics.Records;

namespace Shared.Models.Templates.Pipes.Records
{
    public class PipeTemplateGetAll : IGetAll
    {
        public string EndPointName => StaticClass.PipeTemplates.EndPoint.GetAll;
    }
}
