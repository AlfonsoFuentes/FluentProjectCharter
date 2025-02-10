using Shared.Models.Milestones.Requests;
using Shared.Models.Milestones.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Milestones
{
    public class CreateMilestoneValidator : AbstractValidator<CreateMilestoneRequest>
    {
        private readonly IGenericService Service;

        public CreateMilestoneValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateMilestoneRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateMilestoneRequest validate = new()
            {
                Name = name,
             

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateMilestoneValidator : AbstractValidator<UpdateMilestoneRequest>
    {
        private readonly IGenericService Service;

        public UpdateMilestoneValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateMilestoneRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateMilestoneRequest validate = new()
            {
                Name = name,
          
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
