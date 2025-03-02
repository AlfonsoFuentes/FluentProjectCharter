using Shared.Enums.CurrencyEnums;
using Shared.Models.Suppliers.Requests;
using Shared.Models.Suppliers.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Suppliers
{
    public class CreateSupplierValidator : AbstractValidator<CreateSupplierRequest>
    {
        private readonly IGenericService Service;

        public CreateSupplierValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.NickName).NotEmpty().WithMessage("Nick Name must be defined!");
            RuleFor(x => x.TaxCodeLP).NotEmpty().WithMessage("Tax Code LP must be defined!");
            RuleFor(x => x.TaxCodeLD).NotEmpty().WithMessage("Tax Code LD must be defined!");
            RuleFor(x => x.VendorCode).NotEmpty().WithMessage("Vendor Code must be defined!");
            RuleFor(x => x.SupplierCurrency).NotEqual(CurrencyEnum.None).WithMessage("Supplier currency must be defined!");
            RuleFor(x => x.ContactEmail).EmailAddress().When(x => !string.IsNullOrEmpty(x.ContactEmail)).WithMessage("Valid email must be defined!");
            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

            RuleFor(x => x.VendorCode).MustAsync(ReviewIfVendorCodeExist)
              .When(x => !string.IsNullOrEmpty(x.VendorCode))
              .WithMessage(x => $"{x.VendorCode} already exist");

            RuleFor(x => x.NickName).MustAsync(ReviewIfNickNameExist)
             .When(x => !string.IsNullOrEmpty(x.NickName))
             .WithMessage(x => $"{x.NickName} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateSupplierRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSupplierNameRequest validate = new()
            {
                Name = request.Name,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfVendorCodeExist(CreateSupplierRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSupplierVendorCodeRequest validate = new()
            {
                Name = request.Name,
                VendorCode = request.VendorCode,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfNickNameExist(CreateSupplierRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSupplierNickNameRequest validate = new()
            {
                Name = request.Name,
                NickName = request.NickName,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierRequest>
    {
        private readonly IGenericService Service;

        public UpdateSupplierValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.NickName).NotEmpty().WithMessage("Nick Name must be defined!");
            RuleFor(x => x.TaxCodeLP).NotEmpty().WithMessage("Tax Code LP must be defined!");
            RuleFor(x => x.TaxCodeLD).NotEmpty().WithMessage("Tax Code LD must be defined!");
            RuleFor(x => x.VendorCode).NotEmpty().WithMessage("Vendor Code must be defined!");
            RuleFor(x => x.SupplierCurrency).NotEqual(CurrencyEnum.None).WithMessage("Supplier currency must be defined!");
            RuleFor(x => x.ContactEmail).EmailAddress().When(x=>!string.IsNullOrEmpty(x.ContactEmail)).WithMessage("Valid email must be defined!");
            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

            RuleFor(x => x.VendorCode).MustAsync(ReviewIfVendorCodeExist)
              .When(x => !string.IsNullOrEmpty(x.VendorCode))
              .WithMessage(x => $"{x.VendorCode} already exist");

            RuleFor(x => x.NickName).MustAsync(ReviewIfNickNameExist)
             .When(x => !string.IsNullOrEmpty(x.NickName))
             .WithMessage(x => $"{x.NickName} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateSupplierRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSupplierNameRequest validate = new()
            {
                Name = request.Name,
                Id = request.Id,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfVendorCodeExist(UpdateSupplierRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSupplierVendorCodeRequest validate = new()
            {
                Name = request.Name,
                VendorCode = request.VendorCode,
                Id = request.Id,
            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfNickNameExist(UpdateSupplierRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSupplierNickNameRequest validate = new()
            {
                Name = request.Name,
                NickName = request.NickName,
                Id = request.Id,
            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
