using Shared.Models.BudgetItems.IndividualItems.Foundations.Requests;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Foundations
{
    public class CreateFoundationValidator : AbstractValidator<CreateFoundationRequest>
    {
        private readonly IGenericService Service;

        public CreateFoundationValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");
            

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateFoundationRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateFoundationRequest validate = new()
            {
                Name = name,

                ProjectId = request.ProjectId,



            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateFoundationValidator : AbstractValidator<UpdateFoundationRequest>
    {
        private readonly IGenericService Service;

        public UpdateFoundationValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateFoundationRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateFoundationRequest validate = new()
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
