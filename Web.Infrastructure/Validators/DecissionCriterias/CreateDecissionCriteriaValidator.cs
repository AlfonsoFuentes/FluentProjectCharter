using Shared.Models.DecissionCriterias.Requests;
using Shared.Models.DecissionCriterias.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.DecissionCriterias
{
    public class CreateDecissionCriteriaValidator : AbstractValidator<CreateDecissionCriteriaRequest>
    {
        private readonly IGenericService Service;

        public CreateDecissionCriteriaValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateDecissionCriteriaRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateDecissionCriteriaRequest validate = new()
            {
                Name = name,
                CaseId = request.CaseId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateDecissionCriteriaValidator : AbstractValidator<UpdateDecissionCriteriaRequest>
    {
        private readonly IGenericService Service;

        public UpdateDecissionCriteriaValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateDecissionCriteriaRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateDecissionCriteriaRequest validate = new()
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
