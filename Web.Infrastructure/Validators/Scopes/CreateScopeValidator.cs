using Shared.Models.Scopes.Requests;
using Shared.Models.Scopes.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Scopes
{
    public class CreateScopeValidator : AbstractValidator<CreateScopeRequest>
    {
        private readonly IGenericService Service;

        public CreateScopeValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateScopeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateScopeRequest validate = new()
            {
                Name = name,
                CaseId = request.CaseId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateScopeValidator : AbstractValidator<UpdateScopeRequest>
    {
        private readonly IGenericService Service;

        public UpdateScopeValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateScopeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateScopeRequest validate = new()
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
