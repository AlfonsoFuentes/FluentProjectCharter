


using DocumentFormat.OpenXml.Drawing.Charts;
using Server.Database.Entities;
using Shared.Models.Assumptions.Requests;
using System.Threading;

namespace Server.EndPoint.UpdateDatabases
{

    public static class UpdateDatabaseEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.UpdateDatabase, async (ProjectGetAll Data, IRepository Repository) =>
                {

                    var Context = Repository.Context;



                    try
                    {
                        //   var query = await Context.Set<ExpertJudgement>()
                        //.AsQueryable().IgnoreQueryFilters().ToListAsync();
                        //   if (query.Any())
                        //   {
                        //       foreach (var item in query)
                        //       {
                        //           item.ProjectId = item.ProjectId2;
                        //           await Repository.UpdateAsync(item);
                        //       }

                        //   }
                        var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(null!);
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }



                });


            }

        }



    }

}
