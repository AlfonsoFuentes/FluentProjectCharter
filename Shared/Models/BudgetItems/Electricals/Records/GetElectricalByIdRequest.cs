﻿using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.Electricals.Records
{
    public class GetElectricalByIdRequest : GetByIdMessageResponse, IGetById
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Electricals.EndPoint.GetById;
        public override string ClassName => StaticClass.Electricals.ClassName;
    }

}
