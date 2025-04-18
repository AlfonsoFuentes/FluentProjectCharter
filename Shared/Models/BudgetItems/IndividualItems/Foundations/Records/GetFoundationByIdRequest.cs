﻿using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.BudgetItems.IndividualItems.Foundations.Records
{
    public class GetFoundationByIdRequest : GetByIdMessageResponse, IGetById
    {

        public Guid Id { get; set; }
        public string EndPointName => StaticClass.Foundations.EndPoint.GetById;
        public override string ClassName => StaticClass.Foundations.ClassName;
    }

}
