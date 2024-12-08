using FluentValidation;
using Shared.Models.Brands.Request;
using Web.Infrastructure.Managers.Brands;

namespace Web.Infrastructure.Validators.Brands
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandRequest>
    {
        private readonly IBrandValidatorService Service;

        public CreateBrandValidator(IBrandValidatorService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateBrandRequest request, string name, CancellationToken cancellationToken)
        {
            var result = await Service.ValidateName(new(request.Name));
            return !result;
        }
    }
}
