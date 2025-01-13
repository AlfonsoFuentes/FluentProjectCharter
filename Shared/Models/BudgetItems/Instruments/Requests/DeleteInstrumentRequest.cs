﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Instruments.Requests
{
    public class DeleteInstrumentRequest : DeleteMessageResponse, IRequest
    {
        public string Name { get; set; } = string.Empty;
        public override string Legend => Name;
        public Guid ProjectId { get; set; }
        public override string ClassName => StaticClass.Instruments.ClassName;

        public Guid Id { get; set; }

        public string EndPointName => StaticClass.Instruments.EndPoint.Delete;


    }
}
