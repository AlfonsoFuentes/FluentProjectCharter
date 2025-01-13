using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.ValvesEnum;
using Shared.Models.BudgetItems.Valves.Requests;
using Shared.Models.BudgetItems.Valves.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Valves
{
    public class CreateValveValidator : AbstractValidator<CreateValveRequest>
    {
        private readonly IGenericService Service;

        public CreateValveValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");

            RuleFor(x => x.Model).NotEmpty().When(x => x.ShowDetails)
            .WithMessage("Model must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
           .WithMessage("Material must be defined!");

            RuleFor(x => x.ActuatorType).NotEqual(ActuatorTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.PositionerType).NotEqual(PositionerTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.FailType).NotEqual(FailTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.SignalType).NotEqual(SignalTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.Type).NotEqual(ValveTypesEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

           RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
            .WithMessage("Brand must be defined!");

            RuleFor(x => x.TagLetter).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");

            RuleFor(x => x.Nozzles).Must(x => x.Count > 0).When(x => x.ShowDetails).WithMessage("Nozzles must be greater than zero");
           
            RuleFor(x => x.TagNumber).NotEmpty().When(x => x.ShowDetails)
                .WithMessage("Tag Number must be defined!");

            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
                .MustAsync(ReviewIfTagExist)
               .WithMessage(x => $"{x.Tag} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateValveRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateValveRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(CreateValveRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateValveTagRequest validate = new()
            {
                Name = name,

                Tag = request.Tag,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateValveValidator : AbstractValidator<UpdateValveRequest>
    {
        private readonly IGenericService Service;

        public UpdateValveValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");
            RuleFor(x => x.Model).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Model must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
           .WithMessage("Material must be defined!");

            RuleFor(x => x.ActuatorType).NotEqual(ActuatorTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.PositionerType).NotEqual(PositionerTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.FailType).NotEqual(FailTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.SignalType).NotEqual(SignalTypeEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.Type).NotEqual(ValveTypesEnum.None).When(x => x.ShowDetails)
          .WithMessage("Actuator type must be defined!");

            RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
             .WithMessage("Brand must be defined!");

            RuleFor(x => x.TagLetter).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");

            RuleFor(x => x.Nozzles).Must(x => x.Count > 0).When(x => x.ShowDetails).WithMessage("Nozzles must be greater than zero");
            RuleFor(x => x.TagNumber).NotEmpty().When(x => x.ShowDetails)
                .WithMessage("Tag Number must be defined!");

            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
                .MustAsync(ReviewIfTagExist)
               .WithMessage(x => $"{x.Tag} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateValveRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateValveRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(UpdateValveRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateValveTagRequest validate = new()
            {
                Name = name,

                Tag = request.Tag,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
