﻿using Shared.Models.BudgetItems.IndividualItems.Testings.Requests;
using Shared.Models.BudgetItems.IndividualItems.Testings.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Testings
{
    public class CreateTestingValidator : AbstractValidator<CreateTestingRequest>
    {
        private readonly IGenericService Service;

        public CreateTestingValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");
            

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateTestingRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateTestingRequest validate = new()
            {
                Name = name,

                ProjectId = request.ProjectId,



            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateTestingValidator : AbstractValidator<UpdateTestingRequest>
    {
        private readonly IGenericService Service;

        public UpdateTestingValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateTestingRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateTestingRequest validate = new()
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
