﻿using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.EngineeringDesigns.Requests;

namespace Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Commands
{

    public static class CreateEngineeringDesignEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.EngineeringDesigns.EndPoint.Create, async (CreateEngineeringDesignRequest data, IRepository repository) =>
                {
                    var project = await GetProject(data.ProjectId, repository);
                    if (project == null) return Result.Fail(data.Fail);

                    int order = GetNextOrder(project);

                    var row = EngineeringDesign.Create(project.Id);
                    row.Order = order;

                    data.Map(row);
                    await repository.AddAsync(row);

                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result, data.Succesfully, data.Fail);
                });
            }
            private string[] GetCacheKeys(BudgetItem row)
            {
                List<string> cacheKeys = [
                ..StaticClass.BudgetItems.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
            private async Task<Project?> GetProject(Guid ProjectId, IRepository repository)
            {
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x

                    .Include(d => d.BudgetItems)
                    ;

                Expression<Func<Project, bool>> criteria = d => d.Id == ProjectId;

                return await repository.GetAsync(Criteria: criteria, Includes: includes);
            }

            private int GetNextOrder(Project project)
            {
                var alterations = project.BudgetItems.OfType<EngineeringDesign>();
                return alterations.Any() ? alterations.Max(a => a.Order) + 1 : 1;
            }
        }


        static EngineeringDesign Map(this CreateEngineeringDesignRequest request, EngineeringDesign row)
        {
            row.Name = request.Name;

            row.Budget = request.Budget;
            return row;
        }

    }

}
