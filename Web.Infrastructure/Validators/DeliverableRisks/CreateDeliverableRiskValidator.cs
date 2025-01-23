using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.DeliverableRisks.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.DeliverableRisks
{
    public class CreateDeliverableRiskValidator : AbstractValidator<CreateDeliverableRiskRequest>
    {
        private readonly IGenericService Service;

        public CreateDeliverableRiskValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateDeliverableRiskRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateDeliverableRiskRequest validate = new()
            {
                Name = name,
                ScopeId = request.ScopeId

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateDeliverableRiskValidator : AbstractValidator<UpdateDeliverableRiskRequest>
    {
        private readonly IGenericService Service;

        public UpdateDeliverableRiskValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateDeliverableRiskRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateDeliverableRiskRequest validate = new()
            {
                Name = name,
                ScopeId = request.ScopeId,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
