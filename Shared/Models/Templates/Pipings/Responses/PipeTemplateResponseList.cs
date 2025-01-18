using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Templates.Pipes.Responses
{
    public class PipeTemplateResponseList : IResponseAll
    {
        public List<PipeTemplateResponse> Items { get; set; } = new();
    }
}
