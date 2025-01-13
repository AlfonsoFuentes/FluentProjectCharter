using Shared.Models.Brands.Requests;
using Shared.Models.Brands.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Brands
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandRequest>
    {
        private readonly IGenericService Service;

        public CreateBrandValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateBrandRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateBrandRequest validate = new()
            {
                Name = name,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateBrandValidator : AbstractValidator<UpdateBrandRequest>
    {
        private readonly IGenericService Service;

        public UpdateBrandValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateBrandRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateBrandRequest validate = new()
            {
                Name = name,
               
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
