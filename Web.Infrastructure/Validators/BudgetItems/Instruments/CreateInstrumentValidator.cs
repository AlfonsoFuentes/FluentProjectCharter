using Shared.Models.BudgetItems.Instruments.Requests;
using Shared.Models.BudgetItems.Instruments.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Instruments
{
    public class CreateInstrumentValidator : AbstractValidator<CreateInstrumentRequest>
    {
        private readonly IGenericService Service;

        public CreateInstrumentValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateInstrumentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateInstrumentValidator : AbstractValidator<UpdateInstrumentRequest>
    {
        private readonly IGenericService Service;

        public UpdateInstrumentValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateInstrumentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
