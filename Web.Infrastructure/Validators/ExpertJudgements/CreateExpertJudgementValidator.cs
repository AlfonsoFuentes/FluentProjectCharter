using FluentValidation;
using Shared.Models.ExpertJudgements.Requests;
using Shared.Models.ExpertJudgements.Validators;
using Shared.Models.Projects.Request;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.ExpertJudgements
{
    public class CreateExpertJudgementValidator : AbstractValidator<CreateExpertJudgementRequest>
    {
        private readonly IGenericService Service;

        public CreateExpertJudgementValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

            RuleFor(x => x.Expert).NotNull().WithMessage("Expert must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateExpertJudgementRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateExpertJudgementRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateExpertJudgementValidator : AbstractValidator<UpdateExpertJudgementRequest>
    {
        private readonly IGenericService Service;

        public UpdateExpertJudgementValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

            RuleFor(x => x.Expert).NotNull().WithMessage("Expert must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateExpertJudgementRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateExpertJudgementRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
