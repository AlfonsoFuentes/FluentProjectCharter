using Shared.Models.GanttTasks.Responses;
using Shared.Models.GanttTasks.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.KnownRisks
{
    public class GanttTaskValidator : AbstractValidator<GanttTaskResponse>
    {
        private readonly IGenericService Service;

        public GanttTaskValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(GanttTaskResponse request, string name, CancellationToken cancellationToken)
        {
            ValidateGanttTaskRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
