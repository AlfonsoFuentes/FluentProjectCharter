using FluentValidation;
using Shared.Enums.ProjectNeedTypes;
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
            
            RuleFor(x => x.ProjectNeedType).Must(ReviewProjecNeedType).WithMessage("Type must be defined!");
            RuleFor(x => x.Status).Must(ReviewProjeStatus).WithMessage("Status must be defined!");
            RuleFor(x => x.PercentageContingency).GreaterThan(0).WithMessage("%Contingency must be defined!");
            RuleFor(x => x.PercentageEngineering).GreaterThan(0).WithMessage("%Engineering must be defined!");
            RuleFor(x => x.Sponsor).NotNull().WithMessage("Sponsor must be defined!");
            RuleFor(x => x.Manager).NotNull().WithMessage("Manager must be defined!");
            RuleFor(x => x.InitialProjectDate).NotNull().WithMessage("Initial project date must be defined!");
            RuleFor(x => x.PercentageTaxProductive).GreaterThan(0).When(x => x.IsProductiveAsset == false).WithMessage("%Tax (VAT) must be defined!");

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
        bool ReviewProjecNeedType(CreateProjectRequest request, ProjectNeedTypeEnum need)
        {
            var result = need != ProjectNeedTypeEnum.None;
            return result;
        }
        bool ReviewProjeStatus(CreateProjectRequest request, ProjectStatusEnum need)
        {
            var result = need != ProjectStatusEnum.None;
            return result;
        }
    }
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
    {
        private readonly IGenericService Service;

        public UpdateProjectValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.InitialProjectDate).NotNull().WithMessage("Initial project date must be defined!");
            RuleFor(x => x.ProjectNeedType).Must(ReviewProjecNeedType).WithMessage("Type must be defined!");
            RuleFor(x => x.Sponsor).NotNull().WithMessage("Sponsor must be defined!");
            RuleFor(x => x.Manager).NotNull().WithMessage("Manager must be defined!");
            RuleFor(x => x.Manager).NotNull().WithMessage("Initial project date must be defined!");
            
            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");
            RuleFor(x => x.PercentageContingency).GreaterThan(0).WithMessage("%Contingency must be defined!");
            RuleFor(x => x.PercentageEngineering).GreaterThan(0).WithMessage("%Engineering must be defined!");
            RuleFor(x => x.PercentageTaxProductive).GreaterThan(0).When(x => x.IsProductiveAsset == false).WithMessage("%Tax (VAT) must be defined!");
        }
        bool ReviewProjecNeedType(UpdateProjectRequest request, ProjectNeedTypeEnum need)
        {
            var result = need != ProjectNeedTypeEnum.None;
            return result;
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
    public class ApproveProjectValidator : AbstractValidator<ApproveProjectRequest>
    {
        private readonly IGenericService Service;

        public ApproveProjectValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.InitialProjectDate).NotNull().WithMessage("Initial project date must be defined!");
            RuleFor(x => x.ProjectNeedType).Must(ReviewProjecNeedType).WithMessage("Type must be defined!");
            RuleFor(x => x.Sponsor).NotNull().WithMessage("Sponsor must be defined!");
            RuleFor(x => x.Manager).NotNull().WithMessage("Manager must be defined!");
            RuleFor(x => x.Manager).NotNull().WithMessage("Initial project date must be defined!");
            RuleFor(x => x.ProjectNumber).Matches("^[0-9]*$").WithMessage("Project Number must be number!");
            RuleFor(x => x.ProjectNumber).NotNull().WithMessage("Project Number must be defined");
            RuleFor(x => x.ProjectNumber).NotEmpty().WithMessage("Project Number must be defined");
            RuleFor(x => x.ProjectNumber).Length(5).WithMessage("Project Number must have 5 digits");            

            RuleFor(x => x.ProjectNumber).MustAsync(ReviewIfProjectNumberExist)
                 .When(x => !string.IsNullOrEmpty(x.ProjectNumber))
                 .WithMessage(x => $"{x.ProjectNumber} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");
            RuleFor(x => x.PercentageContingency).GreaterThan(0).WithMessage("%Contingency must be defined!");
            RuleFor(x => x.PercentageEngineering).GreaterThan(0).WithMessage("%Engineering must be defined!");
            RuleFor(x => x.PercentageTaxProductive).GreaterThan(0).When(x => x.IsProductiveAsset == false).WithMessage("%Tax (VAT) must be defined!");
        }
        bool ReviewProjecNeedType(ApproveProjectRequest request, ProjectNeedTypeEnum need)
        {
            var result = need != ProjectNeedTypeEnum.None;
            return result;
        }
      
     
        async Task<bool> ReviewIfNameExist(ApproveProjectRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateProjectRequest validate = new()
            {
                Name = name,

                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfProjectNumberExist(ApproveProjectRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateProjectNumberRequest validate = new()
            {
                ProjectNumber = request.ProjectNumber,

                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
