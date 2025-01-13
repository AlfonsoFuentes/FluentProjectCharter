﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.EngineeringDesigns.Requests
{
    public class DeleteEngineeringDesignRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.EngineeringDesigns.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.EngineeringDesigns.EndPoint.Delete;


    }
}
