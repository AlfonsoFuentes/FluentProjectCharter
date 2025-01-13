﻿using Server.EndPoint.Alterations.Queries;
using Shared.Models.Deliverables.Records;
using Server.EndPoint.AcceptanceCriterias.Queries;
using Server.EndPoint.EHSs.Queries;
using Server.EndPoint.Electricals.Queries;
using Server.EndPoint.EngineeringDesigns.Queries;
using Server.EndPoint.Equipments.Queries;
using Server.EndPoint.Foundations.Queries;
using Server.EndPoint.Instruments.Queries;
using Server.EndPoint.Paintings.Queries;
using Server.EndPoint.Pipings.Queries;
using Server.EndPoint.Structurals.Queries;
using Server.EndPoint.Taxs.Queries;
using Server.EndPoint.Testings.Queries;
using Server.EndPoint.Valves.Queries;
namespace Server.EndPoint.Deliverables.Queries
{
    public static class GetDeliverableByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.GetById, async (GetDeliverableByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> Includes = x => x
                    .Include(x => x.Bennefits)
                    .Include(x => x.Constraints)
                    .Include(x => x.DeliverableRisks)
                    .Include(x => x.Requirements)
                    .Include(x => x.Assumptions)
                    .Include(x => x.AcceptanceCriterias)
              


                    ;

                    Expression<Func<Deliverable, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.SubDeliverables.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map(request.ProjectId);
                    return Result.Success(response);

                });
            }
        }


        public static DeliverableResponse Map(this Deliverable row, Guid ProjectId)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                ScopeId = row.ScopeId,

                Requirements = row.Requirements == null || row.Requirements.Count == 0 ? new() : row.Requirements.Select(x => x.Map(ProjectId)).ToList(),

                IsNodeOpen = row.IsNodeOpen,
                Tab = row.Tab,

                Assumptions = row.Assumptions == null || row.Assumptions.Count == 0 ? new() : row.Assumptions.Select(x => x.Map(ProjectId)).ToList(),

                DeliverableRisks = row.DeliverableRisks == null || row.DeliverableRisks.Count == 0 ? new() :
                row.DeliverableRisks.Select(x => x.Map(ProjectId)).ToList(),

                Constrainsts = row.Constraints == null || row.Constraints.Count == 0 ? new() :
                row.Constraints.Select(x => x.Map(ProjectId)).ToList(),

                Bennefits = row.Bennefits == null || row.Bennefits.Count == 0 ? new() : row.Bennefits.Select(x => x.Map(ProjectId)).ToList(),

                AcceptanceCriterias = row.AcceptanceCriterias == null || row.AcceptanceCriterias.Count == 0 ? new() :
                row.AcceptanceCriterias.Select(x => x.Map(ProjectId)).ToList(),

                ProjectId = ProjectId,

                Alterations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Alteration>().Select(x => x.Map()).ToList(),
                Structurals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Structural>().Select(x => x.Map()).ToList(),
                Foundations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Foundation>().Select(x => x.Map()).ToList(),
                Equipments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Equipment>().Select(x => x.Map()).ToList(),

                Valves = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Valve>().Select(x => x.Map()).ToList(),
                Electricals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Electrical>().Select(x => x.Map()).ToList(),
                Pipings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Isometric>().Select(x => x.Map()).ToList(),
                Instruments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Instrument>().Select(x => x.Map()).ToList(),

                EHSs = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EHS>().Select(x => x.Map()).ToList(),
                Paintings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Painting>().Select(x => x.Map()).ToList(),
                Taxes = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Tax>().Select(x => x.Map()).ToList(),
                Testings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Testing>().Select(x => x.Map()).ToList(),

                EngineeringDesigns = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EngineeringDesign>().Select(x => x.Map()).ToList(),

            };

        }



    }
}
