using Server.Database.Entities.ProjectManagements;
using Shared.Models.LearnedLessons.Requests;

namespace Server.EndPoint.LearnedLessons.Commands
{

    public static class CreateLearnedLessonEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.LearnedLessons.EndPoint.Create, async (CreateLearnedLessonRequest Data, IRepository Repository) =>
                {
                    
                    var lastorder = await Repository.GetLastOrderAsync<LearnedLesson, Project>(Data.ProjectId);

                    var row = LearnedLesson.Create(Data.ProjectId,lastorder);


                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.LearnedLessons.Cache.Key(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static LearnedLesson Map(this CreateLearnedLessonRequest request, LearnedLesson row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
