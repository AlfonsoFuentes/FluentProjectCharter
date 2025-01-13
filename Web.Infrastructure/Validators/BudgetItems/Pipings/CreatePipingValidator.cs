using Shared.Models.BudgetItems.Pipings.Requests;
using Shared.Models.BudgetItems.Pipings.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Pipings
{
    public class CreatePipingValidator : AbstractValidator<CreatePipingRequest>
    {
        private readonly IGenericService Service;

        public CreatePipingValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreatePipingRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipingRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdatePipingValidator : AbstractValidator<UpdatePipingRequest>
    {
        private readonly IGenericService Service;

        public UpdatePipingValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdatePipingRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipingRequest validate = new()
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
