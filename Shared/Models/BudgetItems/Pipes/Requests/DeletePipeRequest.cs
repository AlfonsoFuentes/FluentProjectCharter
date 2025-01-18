﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Pipes.Requests
{
    public class DeletePipeRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.Pipes.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Pipes.EndPoint.Delete;


    }
}
