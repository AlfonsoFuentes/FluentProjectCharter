using Shared.Models.BudgetItems.Electricals.Requests;
using Shared.Models.BudgetItems.Electricals.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Electricals
{
    public class CreateElectricalValidator : AbstractValidator<CreateElectricalRequest>
    {
        private readonly IGenericService Service;

        public CreateElectricalValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");
            

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateElectricalRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateElectricalRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateElectricalValidator : AbstractValidator<UpdateElectricalRequest>
    {
        private readonly IGenericService Service;

        public UpdateElectricalValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.UnitaryCost).GreaterThan(0).WithMessage("Unitary cost must be defined!");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateElectricalRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateElectricalRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
