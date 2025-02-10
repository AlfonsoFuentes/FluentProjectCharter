using Shared.Models.Backgrounds.Requests;
using Shared.Models.Backgrounds.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.BackGrounds
{
    public class CreateBackGroundValidator : AbstractValidator<CreateBackGroundRequest>
    {
        private readonly IGenericService Service;

        public CreateBackGroundValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateBackGroundRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateBackGroundRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateBackGroundValidator : AbstractValidator<UpdateBackGroundRequest>
    {
        private readonly IGenericService Service;

        public UpdateBackGroundValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateBackGroundRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateBackGroundRequest validate = new()
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
