﻿using Shared.Models.BudgetItems.IndividualItems.EHSs.Requests;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Responses;
using Shared.Models.BudgetItems.IndividualItems.EHSs.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.BudgetItems.EHSs
{
   
    public class EHSValidator : AbstractValidator<EHSResponse>
    {
        private readonly IGenericService Service;

        public EHSValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(EHSResponse request, string name, CancellationToken cancellationToken)
        {
            ValidateEHSRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,
                Id = request.Id,
               

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
