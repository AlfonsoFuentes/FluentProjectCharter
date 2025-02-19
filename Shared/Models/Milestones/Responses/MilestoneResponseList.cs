using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Milestones.Responses
{
    public class MilestoneResponseList : IResponseAll
    {
        public List<MilestoneResponse> Items { get; set; } = new();
        public int LastOrder => Items.Count == 0 ? 1 : Items.Max(x => x.Order);
      
    }
}
