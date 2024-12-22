using FluentValidation;
using Shared.Models.HighLevelRequirements.Requests;
using Shared.Models.HighLevelRequirements.Validators;
using Shared.Models.Projects.Request;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.HighLevelRequirements
{
    public class CreateHighLevelRequirementValidator : AbstractValidator<CreateHighLevelRequirementRequest>
    {
        private readonly IGenericService Service;

        public CreateHighLevelRequirementValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

          

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateHighLevelRequirementRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateHighLevelRequirementRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateHighLevelRequirementValidator : AbstractValidator<UpdateHighLevelRequirementRequest>
    {
        private readonly IGenericService Service;

        public UpdateHighLevelRequirementValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

         
            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateHighLevelRequirementRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateHighLevelRequirementRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
