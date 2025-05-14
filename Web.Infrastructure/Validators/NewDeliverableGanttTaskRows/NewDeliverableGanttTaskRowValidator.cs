using Shared.Models.Deliverables.Validators;
using Shared.Models.LearnedLessons.Responses;
using Shared.Models.LearnedLessons.Validators;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.NewDeliverableGanttTaskRows
{
    public class NewDeliverableGanttTaskRowValidator:AbstractValidator<NewDeliverableGanttTaskRow>
    {
        private readonly IGenericService Service;

        public NewDeliverableGanttTaskRowValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }
        async Task<bool> ReviewIfNameExist(NewDeliverableGanttTaskRow request, string name, CancellationToken cancellationToken)
        {
            if (request.IsDeliverable)
            {

                Shared.Models.DeliverableGanttTasks.Validators.ValidateDeliverableRequest validateDeliverable = new()
                {
                    Name = name,

                    ProjectId = request.ProjectId,
                    Id = request.Deliverable.Id

                };
                var resultDeliverable = await Service.Validate(validateDeliverable);
                return !resultDeliverable;
            }
            ValidateNewGanttTaskRequest validate = new()
            {
                Name = name,

                ProjectId = request.ProjectId,
                Id = request.Task.Id,
                DeliverableId = request.Deliverable.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
