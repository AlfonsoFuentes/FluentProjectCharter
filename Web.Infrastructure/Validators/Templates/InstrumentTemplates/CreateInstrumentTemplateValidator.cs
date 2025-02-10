using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Instruments;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.Templates.Instruments.Requests;
using Shared.Models.Templates.Instruments.Validators;
using Shared.Models.Templates.NozzleTemplates;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Templates.InstrumentTemplates
{
    public class CreateInstrumentTemplateValidator : AbstractValidator<CreateInstrumentTemplateRequest>
    {
        private readonly IGenericService Service;

        public CreateInstrumentTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Value must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty()
              .WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Type).NotEmpty()
           .WithMessage("Variable must be defined!");

            RuleFor(x => x.SubType).NotEmpty()
        .WithMessage("Modifier Variable must be defined!");

            RuleFor(x => x.Brand).NotEmpty()
           .WithMessage("Brand must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None)
          .WithMessage("Material must be defined!");



            RuleFor(x => x.Model).NotEmpty()
          .WithMessage("Model must be defined!");

            RuleFor(x => x.Reference).NotEmpty()
          .WithMessage("Reference must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewInlet).When(x =>
               (x.Type.Id != VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id != VariableInstrumentEnum.MassFlowMeter.Id))
                   .WithMessage("Nozzles must be have one inlet");

            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x => 
               (x.Type.Id == VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id == VariableInstrumentEnum.MassFlowMeter.Id))
                  .WithMessage("Nozzles must be have one inlet and one outlet");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType)
               .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter)
                .WithMessage("All diameter nozzle must be defined!");



            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

   

        }

        async Task<bool> ReviewIfNameExist(CreateInstrumentTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentTemplateRequest validate = new()
            {
                Name = name,
                Brand = request.BrandResponse!.Name,
                Model = request.Model,
                Material = request.Material.Name,
                SignalType = request.SignalType.Name,
                Type = request.Type.Name,
                SubType = request.SubType.Name,
                TagLetter = request.TagLetter,
                Reference = request.Reference,
                 



            };
            var result = await Service.Validate(validate);
            return !result;
        }
        bool ReviewInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Outlet.Id)) return false;

            return true;
        }
        bool ReviewInlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;

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
    public class UpdateInstrumentTemplateValidator : AbstractValidator<UpdateInstrumentTemplateRequest>
    {
        private readonly IGenericService Service;

        public UpdateInstrumentTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Value must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty()
              .WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Type).NotEmpty()
           .WithMessage("Variable must be defined!");

            RuleFor(x => x.SubType).NotEmpty()
        .WithMessage("Modifier Variable must be defined!");

            RuleFor(x => x.Brand).NotEmpty()
           .WithMessage("Brand must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None)
          .WithMessage("Material must be defined!");



            RuleFor(x => x.Model).NotEmpty()
          .WithMessage("Model must be defined!");

            RuleFor(x => x.Reference).NotEmpty()
          .WithMessage("Reference must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewInlet).When(x =>
               (x.Type.Id != VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id != VariableInstrumentEnum.MassFlowMeter.Id))
                   .WithMessage("Nozzles must be have one inlet");

            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x =>
               (x.Type.Id == VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id == VariableInstrumentEnum.MassFlowMeter.Id))
                  .WithMessage("Nozzles must be have one inlet and one outlet");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType)
               .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter)
                .WithMessage("All diameter nozzle must be defined!");



            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        

        }

        async Task<bool> ReviewIfNameExist(UpdateInstrumentTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentTemplateRequest validate = new()
            {
                Name = name,
                Brand = request.BrandResponse!.Name,
                Model = request.Model,
                Material = request.Material.Name,
                SignalType = request.SignalType.Name,
                Type = request.Type.Name,
                SubType = request.SubType.Name,
                TagLetter = request.TagLetter,
                Reference = request.Reference,




            };
            var result = await Service.Validate(validate);
            return !result;
        }
        bool ReviewInletOutlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Outlet.Id)) return false;

            return true;
        }
        bool ReviewInlet(List<NozzleTemplateResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;

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
