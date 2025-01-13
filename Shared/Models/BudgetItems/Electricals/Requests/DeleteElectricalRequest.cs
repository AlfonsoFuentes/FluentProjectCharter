﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Electricals.Requests
{
    public class DeleteElectricalRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.Electricals.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Electricals.EndPoint.Delete;


    }
}
