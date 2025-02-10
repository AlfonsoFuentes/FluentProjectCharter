using Shared.Models.BudgetItems.Taxs.Requests;
using Shared.Models.BudgetItems.Taxs.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Taxs
{
    public class CreateTaxValidator : AbstractValidator<CreateTaxRequest>
    {
        private readonly IGenericService Service;

        public CreateTaxValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

            RuleFor(x => x.Budget).NotEqual(0).WithMessage("Budget must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateTaxRequest request, string name, CancellationToken cancellationToken)
        {
            try
            {
                ValidateTaxRequest validate = new()
                {
                    Name = name,
                    ProjectId = request.ProjectId,

                };
                var result = await Service.Validate(validate);
                return !result;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return false;

        }
    }
    public class UpdateTaxValidator : AbstractValidator<UpdateTaxRequest>
    {
        private readonly IGenericService Service;

        public UpdateTaxValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                          .When(x => !string.IsNullOrEmpty(x.Name))
                          .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateTaxRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateTaxRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,
                Id = request.Id,
 

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
