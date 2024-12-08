using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Deliverables
{
    public class CreateDeliverableValidator : AbstractValidator<CreateDeliverableRequest>
    {
        private readonly IGenericService Service;

        public CreateDeliverableValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateDeliverableRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateDeliverableRequest validate = new()
            {
                Name = name,
                ScopeId = request.ScopeId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateDeliverableValidator : AbstractValidator<UpdateDeliverableRequest>
    {
        private readonly IGenericService Service;

        public UpdateDeliverableValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateDeliverableRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateDeliverableRequest validate = new()
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
