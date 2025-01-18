using FluentValidation;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.BudgetItems.Instruments.Requests;
using Shared.Models.BudgetItems.Instruments.Validators;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Instruments
{
    public class CreateInstrumentValidator : AbstractValidator<CreateInstrumentRequest>
    {
        private readonly IGenericService Service;

        public CreateInstrumentValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Type).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Variable must be defined!");

            RuleFor(x => x.SubType).NotEmpty().When(x => x.ShowDetails)
        .WithMessage("Modifier Variable must be defined!");

            RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Brand must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Material must be defined!");



            RuleFor(x => x.Model).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Model must be defined!");

            RuleFor(x => x.Reference).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Reference must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewInlet).When(x => x.ShowDetails
               && (x.Type.Id != VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id != VariableInstrumentEnum.MassFlowMeter.Id))
                   .WithMessage("Nozzles must be have one inlet");

            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x => x.ShowDetails
              && (x.Type.Id == VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id == VariableInstrumentEnum.MassFlowMeter.Id))
                  .WithMessage("Nozzles must be have one inlet and one outlet");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType).When(x => x.ShowDetails)
               .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter).When(x => x.ShowDetails)
                .WithMessage("All diameter nozzle must be defined!");


            RuleFor(x => x.TagNumber).NotEmpty().When(x => x.ShowDetails)
                .WithMessage("Tag Number must be defined!");

            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
                .MustAsync(ReviewIfTagExist)
               .WithMessage(x => $"{x.Tag} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(CreateInstrumentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(CreateInstrumentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentTagRequest validate = new()
            {
                Name = name,

                Tag = request.Tag,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
        bool ReviewInletOutlet(List<NozzleResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Outlet.Id)) return false;

            return true;
        }
        bool ReviewInlet(List<NozzleResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;

            return true;
        }
        bool ReviewConnectionType(List<NozzleResponse> nozzles)
        {
            if (nozzles.Any(x => x.ConnectionType.Id == ConnectionTypeEnum.None.Id)) return false;

            return true;
        }
        bool ReviewDiameter(List<NozzleResponse> nozzles)
        {
            if (nozzles.Any(x => x.NominalDiameter.Id == NominalDiameterEnum.None.Id)) return false;

            return true;
        }
    }
    public class UpdateInstrumentValidator : AbstractValidator<UpdateInstrumentRequest>
    {
        private readonly IGenericService Service;

        public UpdateInstrumentValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Type).NotEmpty().When(x => x.ShowDetails)
             .WithMessage("Variable must be defined!");

            RuleFor(x => x.SubType).NotEmpty().When(x => x.ShowDetails)
        .WithMessage("Modifier Variable must be defined!");

            RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Brand must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.Model).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Model must be defined!");

            RuleFor(x => x.Reference).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Reference must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewInlet).When(x => x.ShowDetails
            && (x.Type.Id != VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id != VariableInstrumentEnum.MassFlowMeter.Id))
                .WithMessage("Nozzles must be have one inlet");

            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x => x.ShowDetails
              && (x.Type.Id == VariableInstrumentEnum.VolumeFlowMeter.Id || x.Type.Id == VariableInstrumentEnum.MassFlowMeter.Id))
                  .WithMessage("Nozzles must be have one inlet and one outlet");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType).When(x => x.ShowDetails)
               .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter).When(x => x.ShowDetails)
                .WithMessage("All diameter nozzle must be defined!");

            RuleFor(x => x.TagNumber).NotEmpty().When(x => x.ShowDetails)
                .WithMessage("Tag Number must be defined!");


            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
                .MustAsync(ReviewIfTagExist)
               .WithMessage(x => $"{x.Tag} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdateInstrumentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(UpdateInstrumentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateInstrumentTagRequest validate = new()
            {
                Name = name,
                Id = request.Id,
                Tag = request.Tag,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
        bool ReviewInletOutlet(List<NozzleResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Outlet.Id)) return false;

            return true;
        }
        bool ReviewInlet(List<NozzleResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;

            return true;
        }
        bool ReviewConnectionType(List<NozzleResponse> nozzles)
        {
            if (nozzles.Any(x => x.ConnectionType.Id == ConnectionTypeEnum.None.Id)) return false;

            return true;
        }
        bool ReviewDiameter(List<NozzleResponse> nozzles)
        {
            if (nozzles.Any(x => x.NominalDiameter.Id == NominalDiameterEnum.None.Id)) return false;

            return true;
        }
    }
}
