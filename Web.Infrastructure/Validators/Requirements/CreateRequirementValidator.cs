using Shared.Models.Requirements.Requests;
using Shared.Models.Requirements.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Requirements
{
    public class CreateRequirementValidator : AbstractValidator<CreateRequirementRequest>
    {
        private readonly IGenericService Service;

        public CreateRequirementValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateRequirementRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateRequirementRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateRequirementValidator : AbstractValidator<UpdateRequirementRequest>
    {
        private readonly IGenericService Service;

        public UpdateRequirementValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateRequirementRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateRequirementRequest validate = new()
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
