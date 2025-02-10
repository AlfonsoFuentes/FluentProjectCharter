using Shared.Models.LearnedLessons.Requests;


namespace Server.EndPoint.LearnedLessons.Commands
{
    public static class UpdateLearnedLessonEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.LearnedLessons.EndPoint.Update, async (UpdateLearnedLessonRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<LearnedLesson>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Projects.Cache.Key(row.ProjectId), .. StaticClass.LearnedLessons.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
        }


        static LearnedLesson Map(this UpdateLearnedLessonRequest request, LearnedLesson row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
