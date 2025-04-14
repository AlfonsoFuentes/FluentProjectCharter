using Shared.Enums.CurrencyEnums;
using Shared.Models.Projects.Validators;
using Shared.Models.PurchaseOrders.Requests;
using Shared.Models.PurchaseOrders.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.PurchaseOrders
{
    public class CreatePurchaseOrderValidator : AbstractValidator<CreatePurchaseOrderRequest>
    {
        private readonly IGenericService Service;
        public CreatePurchaseOrderValidator(IGenericService _Service)
        {
            Service = _Service;

            RuleFor(x => x.Supplier).NotNull().WithMessage("Supplier must be defined");


            RuleFor(X => X.Name).NotEmpty().WithMessage("Purchase order name must be defined");
            RuleFor(X => X.Name).NotNull().WithMessage("Purchase order name must be defined");

            RuleFor(X => X.QuoteNo).NotEmpty().WithMessage("Quote name must be defined");
            RuleFor(X => X.QuoteNo).NotNull().WithMessage("Quote name must be defined");

            RuleFor(X => X.PurchaseRequisition).NotEmpty().WithMessage("PR must be defined");
            RuleFor(X => X.PurchaseRequisition).NotNull().WithMessage("PR must be defined");

            RuleFor(X => X.PurchaseRequisition)
                 .Must(x => x.StartsWith("PR"))
                 .When(x => !string.IsNullOrEmpty(x.PurchaseRequisition))
                 .WithMessage("PR must include PR letter at start");
            RuleFor(x => x.CurrencyDate).NotNull().WithMessage("Currency date must be defined");

            RuleFor(x => x.USDEUR).GreaterThan(0).WithMessage("TRM must be defined");
            RuleFor(x => x.USDCOP).GreaterThan(0).WithMessage("TRM must be defined");

            RuleFor(x => x.TotalUSD).GreaterThan(0).WithMessage("PO Value must be defined");

            RuleFor(x => x.IsAnyValueNotDefined).NotEqual(true).WithMessage("All Item must have Currency value greater Than zero");
            RuleFor(x => x.IsAnyNameEmpty).NotEqual(true).WithMessage("All purchase orders items must have a Name");

            RuleFor(x => x.QuoteCurrency).Must(x => x.Id != CurrencyEnum.None.Id).WithMessage("Quote currency must be defined");

            RuleFor(x => x.Name).MustAsync(ReviewNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name)).WithMessage(x => $"{x.Name} already exist"); ;

            RuleFor(x => x.PurchaseRequisition).MustAsync(ReviewPRExist)
                .When(x => !string.IsNullOrEmpty(x.PurchaseRequisition)).WithMessage(x => $"{x.PurchaseRequisition} already exist");


            RuleForEach(x => x.PurchaseOrderItems).ChildRules(order =>
            {
                order.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be defined");
                order.RuleFor(x => x.UnitaryQuoteCurrency).GreaterThan(0).WithMessage("Unitary value must be defined");
                order.RuleFor(x => x.BudgetItemId).NotEqual(Guid.Empty).WithMessage("Budget Item must be defined");
                order.RuleFor(x => x.Name).NotEmpty().WithMessage("Item Name must be defined");
                order.RuleFor(x => x.Name).NotNull().WithMessage("Item Name must be defined");
            });


        }
        async Task<bool> ReviewNameExist(CreatePurchaseOrderRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePurchaseOrderNameRequest validate = new()
            {
                Name = request.Name,

            };
            var result = await Service.Validate(validate);


            return !result;
        }
        async Task<bool> ReviewPRExist(CreatePurchaseOrderRequest request, string pr, CancellationToken cancellationToken)
        {

            ValidatePurchaseOrderRequisitionRequest validate = new()
            {
                PurchaseRequisition = request.PurchaseRequisition,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }

}
