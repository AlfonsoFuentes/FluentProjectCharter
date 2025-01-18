using Shared.Enums.Materials;
using Shared.Models.Templates.Valves.Requests;
using Shared.Models.Templates.Valves.Validators;
using Shared.Models.Templates.NozzleTemplates;
using Web.Infrastructure.Managers.Generic;
using FluentValidation;
using Shared.Enums.ValvesEnum;
using Shared.Enums.DiameterEnum;
using Shared.Enums.NozzleTypes;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Enums.ConnectionTypes;

namespace Web.Infrastructure.Validators.Templates.ValveTemplates
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

            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x =>
          x.Type.Id != ValveTypesEnum.Three_Way.Id || x.Type.Id != ValveTypesEnum.Four_Way.Id
          ).WithMessage("Nozzles must be have one inlet and one outlet");

            RuleFor(x => x.Nozzles).Must(ReviewThreeWayInletOutlet).When(x =>
           x.Type.Id == ValveTypesEnum.Three_Way.Id
           ).WithMessage("Nozzles must be have one inlet and one outlet and three nozzles");


            RuleFor(x => x.Nozzles).Must(ReviewFourWayInletOutlet).When(x =>
           x.Type.Id == ValveTypesEnum.Four_Way.Id
           ).WithMessage("Nozzles must be have one inlet and one outlet and four nozzles");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType)
               .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter)
                .WithMessage("All diameter nozzle must be defined!");

            RuleFor(x => x.BrandResponse).NotNull().WithMessage("Brand must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
               .When(x => !string.IsNullOrEmpty(x.Brand))
               .WithMessage(x => $"Template already exist");

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
        bool ReviewInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            return true;
        }
        bool ReviewThreeWayInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (nozzles.Count < 3) return false;

            return true;
        }
        bool ReviewFourWayInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (nozzles.Count < 4) return false;

            return true;
        }
        bool ReviewConnectionType(List<NozzleTemplateResponse> nozzles)
        {
            if (nozzles.Any(x => x.ConnectionType.Id == ConnectionTypeEnum.None.Id)) return false;

            return true;
        }
        bool ReviewDiameter(List<NozzleTemplateResponse> nozzles)
        {
            if (nozzles.Any(x => x.NominalDiameter.Id == NominalDiameterEnum.None.Id)) return false;

            return true;
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

            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x =>
         x.Type.Id != ValveTypesEnum.Three_Way.Id || x.Type.Id != ValveTypesEnum.Four_Way.Id
         ).WithMessage("Nozzles must be have one inlet and one outlet");

            RuleFor(x => x.Nozzles).Must(ReviewThreeWayInletOutlet).When(x =>
           x.Type.Id == ValveTypesEnum.Three_Way.Id
           ).WithMessage("Nozzles must be have one inlet and one outlet and three nozzles");


            RuleFor(x => x.Nozzles).Must(ReviewFourWayInletOutlet).When(x =>
           x.Type.Id == ValveTypesEnum.Four_Way.Id
           ).WithMessage("Nozzles must be have one inlet and one outlet and four nozzles");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType)
               .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter)
                .WithMessage("All diameter nozzle must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Brand))
                .WithMessage(x => $"Template already exist");

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
        bool ReviewInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            return true;
        }
        bool ReviewThreeWayInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (nozzles.Count < 3) return false;

            return true;
        }
        bool ReviewFourWayInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (nozzles.Count < 4) return false;

            return true;
        }
        bool ReviewConnectionType(List<NozzleTemplateResponse> nozzles)
        {
            if (nozzles.Any(x => x.ConnectionType.Id == ConnectionTypeEnum.None.Id)) return false;

            return true;
        }
        bool ReviewDiameter(List<NozzleTemplateResponse> nozzles)
        {
            if (nozzles.Any(x => x.NominalDiameter.Id == NominalDiameterEnum.None.Id)) return false;

            return true;
        }
    }
}
