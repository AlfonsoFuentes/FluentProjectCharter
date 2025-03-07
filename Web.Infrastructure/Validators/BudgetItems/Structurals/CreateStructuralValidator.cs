using Shared.Models.BudgetItems.IndividualItems.Structurals.Requests;
using Shared.Models.BudgetItems.IndividualItems.Structurals.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Structurals
{
    public class CreateStructuralValidator : AbstractValidator<CreateStructuralRequest>
    {
        private readonly IGenericService Service;

        public CreateStructuralValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");
            

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateStructuralRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateStructuralRequest validate = new()
            {
                Name = name,

                ProjectId = request.ProjectId,



            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateStructuralValidator : AbstractValidator<UpdateStructuralRequest>
    {
        private readonly IGenericService Service;

        public UpdateStructuralValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateStructuralRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateStructuralRequest validate = new()
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
