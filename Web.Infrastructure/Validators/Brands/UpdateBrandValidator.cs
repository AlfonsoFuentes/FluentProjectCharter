
using Shared.Models.Brands.Request;
using Web.Infrastructure.Managers.Brands;

namespace Web.Infrastructure.Validators.Brands
{
    public class UpdateBrandValidator : AbstractValidator<UpdateBrandRequest>
    {
        private readonly IBrandValidatorService Service;

        public UpdateBrandValidator(IBrandValidatorService service)
        {
            Service = service;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Brand must be defined!")
                .NotNull().WithMessage("Brand must be defined!");
            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist).WithMessage(x => $"{x.Name} already exist");



        }

        async Task<bool> ReviewIfNameExist(UpdateBrandRequest request, string name, CancellationToken cancellationToken)
        {

            var result = await Service.ValidateName(new(request.Name, request.BrandId));
            return !result;
        }
    }
}

