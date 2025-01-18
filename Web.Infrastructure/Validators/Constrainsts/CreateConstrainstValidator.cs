using Shared.Models.Constrainsts.Requests;
using Shared.Models.Constrainsts.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Constrainsts
{
    public class CreateConstrainstValidator : AbstractValidator<CreateConstrainstRequest>
    {
        private readonly IGenericService Service;

        public CreateConstrainstValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateConstrainstRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateConstrainstRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,
          

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateConstrainstValidator : AbstractValidator<UpdateConstrainstRequest>
    {
        private readonly IGenericService Service;

        public UpdateConstrainstValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateConstrainstRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateConstrainstRequest validate = new()
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
