using Shared.Models.KnownRisks.Requests;
using Shared.Models.KnownRisks.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.KnownRisks
{
    public class CreateKnownRiskValidator : AbstractValidator<CreateKnownRiskRequest>
    {
        private readonly IGenericService Service;

        public CreateKnownRiskValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateKnownRiskRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateKnownRiskRequest validate = new()
            {
                Name = name,
                CaseId = request.CaseId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateKnownRiskValidator : AbstractValidator<UpdateKnownRiskRequest>
    {
        private readonly IGenericService Service;

        public UpdateKnownRiskValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateKnownRiskRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateKnownRiskRequest validate = new()
            {
                Name = name,
                CaseId = request.CaseId,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
