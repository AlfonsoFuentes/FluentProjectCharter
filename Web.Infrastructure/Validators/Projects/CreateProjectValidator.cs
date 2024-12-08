using Shared.Models.Projects.Request;
using Shared.Models.Projects.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Projects
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
    {
        private readonly IGenericService Service;

        public CreateProjectValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateProjectRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateProjectRequest validate = new()
            {
                Name = name,
            


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
    {
        private readonly IGenericService Service;

        public UpdateProjectValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateProjectRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateProjectRequest validate = new()
            {
                Name = name,

                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
