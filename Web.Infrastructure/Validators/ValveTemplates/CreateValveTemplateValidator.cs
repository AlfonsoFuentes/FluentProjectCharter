using Shared.Enums.Materials;
using Shared.Models.Templates.Valves.Requests;
using Shared.Models.Templates.Valves.Validators;
using Shared.Models.Templates.NozzleTemplates;
using Web.Infrastructure.Managers.Generic;
using FluentValidation;
using Shared.Enums.ValvesEnum;
using Shared.Enums.DiameterEnum;

namespace Web.Infrastructure.Validators.ValveTemplates
{
    public class CreateValveTemplateValidator : AbstractValidator<CreateValveTemplateRequest>
    {
        private readonly IGenericService Service;

        public CreateValveTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Value must be defined!");
            RuleFor(x => x.Type).NotEqual(ValveTypesEnum.None).WithMessage("Type must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model must be defined!");
            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).WithMessage("Material must be defined!");
            RuleFor(x => x.ActuatorType).NotEqual(ActuatorTypeEnum.None).WithMessage("Actuator Type Material must be defined!");
            RuleFor(x => x.PositionerType).NotEqual(PositionerTypeEnum.None).WithMessage("Positioner Type must be defined!");
            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).WithMessage("Diameter must be defined!");
            RuleFor(x => x.FailType).NotEqual(FailTypeEnum.None).WithMessage("Fail Type must be defined!");
            RuleFor(x => x.SignalType).NotEqual(SignalTypeEnum.None).WithMessage("Signal Type must be defined!");


            RuleFor(x => x.Nozzles).Must(ReviewIfNozzleCount).WithMessage("Nozzles must be greater than zero");
            RuleFor(x => x.BrandResponse).NotNull().WithMessage("Brand must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
               .When(x => !string.IsNullOrEmpty(x.Brand))
               .WithMessage(x => $"Template already exist");

        }
        bool ReviewIfNozzleCount(List<NozzleTemplateResponse> nozzles)
        {
            return nozzles.Count > 0;
        }
        async Task<bool> ReviewIfNameExist(CreateValveTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateValveTemplateRequest validate = new()
            {
                Name = name,
                Brand = request.BrandResponse!.Name,
                Model = request.Model,
                ActuadorType = request.ActuatorType.Name,
                Diameter = request.Diameter.Name,
                FailType = request.FailType.Name,
                HasFeedBack = request.HasFeedBack,
                Material = request.Material.Name,
                PositionerType = request.PositionerType.Name,
                SignalType = request.SignalType.Name,
                TagLetter = request.TagLetter,
                Type = request.Type.Name,
            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateValveTemplateValidator : AbstractValidator<UpdateValveTemplateRequest>
    {
        private readonly IGenericService Service;

        public UpdateValveTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Value must be defined!");
            RuleFor(x => x.Type).NotEqual(ValveTypesEnum.None).WithMessage("Type must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model must be defined!");
            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).WithMessage("Material must be defined!");
            RuleFor(x => x.ActuatorType).NotEqual(ActuatorTypeEnum.None).WithMessage("Actuator Type Material must be defined!");
            RuleFor(x => x.PositionerType).NotEqual(PositionerTypeEnum.None).WithMessage("Positioner Type must be defined!");
            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).WithMessage("Diameter must be defined!");
            RuleFor(x => x.FailType).NotEqual(FailTypeEnum.None).WithMessage("Fail Type must be defined!");
            RuleFor(x => x.SignalType).NotEqual(SignalTypeEnum.None).WithMessage("Signal Type must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Brand))
                .WithMessage(x => $"Template already exist");

        }
        bool ReviewIfNozzleCount(List<NozzleTemplateResponse> nozzles)
        {
            return nozzles.Count > 0;
        }
        async Task<bool> ReviewIfNameExist(UpdateValveTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateValveTemplateRequest validate = new()
            {
                Name = name,
                Brand = request.BrandResponse!.Name,
                Model = request.Model,
                ActuadorType = request.ActuatorType.Name,
                Diameter = request.Diameter.Name,
                FailType = request.FailType.Name,
                HasFeedBack = request.HasFeedBack,
                Material = request.Material.Name,
                PositionerType = request.PositionerType.Name,
                SignalType = request.SignalType.Name,
                TagLetter = request.TagLetter,
                Type = request.Type.Name,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
