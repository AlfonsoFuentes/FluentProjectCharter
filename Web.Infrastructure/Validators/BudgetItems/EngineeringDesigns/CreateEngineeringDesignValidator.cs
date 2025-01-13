using Shared.Models.BudgetItems.EngineeringDesigns.Requests;
using Shared.Models.BudgetItems.EngineeringDesigns.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.EngineeringDesigns
{
    public class CreateEngineeringDesignValidator : AbstractValidator<CreateEngineeringDesignRequest>
    {
        private readonly IGenericService Service;

        public CreateEngineeringDesignValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateEngineeringDesignRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEngineeringDesignRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateEngineeringDesignValidator : AbstractValidator<UpdateEngineeringDesignRequest>
    {
        private readonly IGenericService Service;

        public UpdateEngineeringDesignValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");
            

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateEngineeringDesignRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEngineeringDesignRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
