using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Constrainsts.Responses
{
    public class ConstrainstResponseList : IResponseAll
    {
        public List<ConstrainstResponse> Items { get; set; } = new();
        public string ProjectName { get; set; } = string.Empty;
    }
}
