﻿using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.IndividualItems.Equipments.Validators;

namespace Server.EndPoint.BudgetItems.IndividualItems.Equipments.Validators
{
    public static class ValidateEquipmentsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Equipments.EndPoint.Validate, async (ValidateEquipmentRequest Data, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                      .Include(x => x.BudgetItems)
                      ;
                    Expression<Func<Project, bool>> CriteriaDeliverable = x => x.Id == Data.ProjectId;
                    string CacheKey = StaticClass.Projects.Cache.GetById(Data.ProjectId);

                    var project = await Repository.GetAsync(Cache: CacheKey, Criteria: CriteriaDeliverable, Includes: Includes);
                    if (project == null) return false;
                    var budgetitems = project.BudgetItems;
                    if (budgetitems == null) return false;


                    Func<BudgetItem, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);

                    var result = budgetitems.Any(x => CriteriaExist(x));

                    return result;
                });


            }
        }



    }

}
