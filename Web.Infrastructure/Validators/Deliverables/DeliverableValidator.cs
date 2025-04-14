using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Deliverables.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Deliverables
{
  
    public class DeliverableValidator : AbstractValidator<DeliverableResponse>
    {
        private readonly IGenericService Service;

        public DeliverableValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(DeliverableResponse request, string name, CancellationToken cancellationToken)
        {
            ValidateDeliverableRequest validate = new()
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
