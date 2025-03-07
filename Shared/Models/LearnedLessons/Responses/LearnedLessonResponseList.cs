using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.LearnedLessons.Responses
{
    public class LearnedLessonResponseList : IResponseAll
    {
        public List<LearnedLessonResponse> Items { get; set; } = new();
        public string ProjectName { get; set; } = string.Empty;
    }
}
