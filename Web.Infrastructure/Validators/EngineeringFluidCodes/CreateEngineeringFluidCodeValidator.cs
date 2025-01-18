using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.EngineeringFluidCodes
{
    public class CreateEngineeringFluidCodeValidator : AbstractValidator<CreateEngineeringFluidCodeRequest>
    {
        private readonly IGenericService Service;

        public CreateEngineeringFluidCodeValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code must be defined!");
            RuleFor(x => x.Code).Length(1, 3).WithMessage("The fluid code cannot be more than 3 characters");
            
            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateEngineeringFluidCodeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEngineeringFluidCodeRequest validate = new()
            {
                Name = name,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateEngineeringFluidCodeValidator : AbstractValidator<UpdateEngineeringFluidCodeRequest>
    {
        private readonly IGenericService Service;

        public UpdateEngineeringFluidCodeValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code must be defined!");
            RuleFor(x => x.Code).Length(1, 3).WithMessage("The fluid code cannot be more than 3 characters");
            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateEngineeringFluidCodeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEngineeringFluidCodeRequest validate = new()
            {
                Name = name,

                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
