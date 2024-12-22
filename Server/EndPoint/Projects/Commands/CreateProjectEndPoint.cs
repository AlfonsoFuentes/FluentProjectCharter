


using Server.Database.Entities;
using System.Threading;

namespace Server.EndPoint.Projects.Commands
{

    public static class CreateProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.Create, async (CreateProjectRequest Data, IRepository Repository) =>
                {
                    var row = Project.Create();

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Projects.Cache.Key(row.Id));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Project Map(this CreateProjectRequest request, Project row)
        {
            row.Name = request.Name;
            row.ProjectNeedType=request.ProjectNeedType.Name;
            row.InitialBudget=request.InitialBudget;
            row.ProjectDescription=request.ProjectDescription;
            row.ManagerId=request.Manager==null?null:request.Manager.Id;
            row.SponsorId=request.Sponsor==null?null:request.Sponsor.Id;
            row.Version0Date = request.InitialProjectDate == null ? null : request.InitialProjectDate.Value;
            return row;
        }

    }

}
