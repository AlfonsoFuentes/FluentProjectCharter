using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.OrganizationStrategys
{
    public class CreateOrganizationStrategyValidator : AbstractValidator<CreateOrganizationStrategyRequest>
    {
        private readonly IGenericService Service;

        public CreateOrganizationStrategyValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateOrganizationStrategyRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateOrganizationStrategyRequest validate = new()
            {
                Name = name,
             

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateOrganizationStrategyValidator : AbstractValidator<UpdateOrganizationStrategyRequest>
    {
        private readonly IGenericService Service;

        public UpdateOrganizationStrategyValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateOrganizationStrategyRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateOrganizationStrategyRequest validate = new()
            {
                Name = name,
          
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
