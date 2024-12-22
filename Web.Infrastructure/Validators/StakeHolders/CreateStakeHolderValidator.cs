using FluentValidation;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.BackGrounds
{
    public class CreateStakeHolderValidator : AbstractValidator<CreateStakeHolderRequest>
    {
        private readonly IGenericService Service;

        public CreateStakeHolderValidator(IGenericService service)
        {
            Service = service;
           
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be defined!");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Area must be defined!");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateStakeHolderRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateStakeHolderRequest validate = new()
            {
                Name = name,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
        
    }
    public class UpdateStakeHolderValidator : AbstractValidator<UpdateStakeHolderRequest>
    {
        private readonly IGenericService Service;

        public UpdateStakeHolderValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be defined!");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Area must be defined!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid!");
          
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateStakeHolderRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateStakeHolderRequest validate = new()
            {
                Name = name,
             
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
       
    }
}
