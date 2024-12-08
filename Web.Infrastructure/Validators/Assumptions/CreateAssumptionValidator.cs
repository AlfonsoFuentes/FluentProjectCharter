using Shared.Models.Assumptions.Requests;
using Shared.Models.Assumptions.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Assumptions
{
    public class CreateAssumptionValidator : AbstractValidator<CreateAssumptionRequest>
    {
        private readonly IGenericService Service;

        public CreateAssumptionValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateAssumptionRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateAssumptionRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateAssumptionValidator : AbstractValidator<UpdateAssumptionRequest>
    {
        private readonly IGenericService Service;

        public UpdateAssumptionValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateAssumptionRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateAssumptionRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
