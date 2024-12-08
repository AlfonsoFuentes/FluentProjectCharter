using Shared.Models.SucessfullFactors.Requests;
using Shared.Models.SucessfullFactors.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.SucessfullFactors
{
    public class CreateSucessfullFactorValidator : AbstractValidator<CreateSucessfullFactorRequest>
    {
        private readonly IGenericService Service;

        public CreateSucessfullFactorValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateSucessfullFactorRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSucessfullFactorRequest validate = new()
            {
                Name = name,
                CaseId = request.CaseId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateSucessfullFactorValidator : AbstractValidator<UpdateSucessfullFactorRequest>
    {
        private readonly IGenericService Service;

        public UpdateSucessfullFactorValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateSucessfullFactorRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateSucessfullFactorRequest validate = new()
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
