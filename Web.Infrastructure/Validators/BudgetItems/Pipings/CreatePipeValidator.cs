using FluentValidation;
using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.BudgetItems.Pipes.Requests;
using Shared.Models.BudgetItems.Pipes.Validators;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.Templates.NozzleTemplates;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.BudgetItems.Pipings
{
    public class CreatePipeValidator : AbstractValidator<CreatePipeRequest>
    {
        private readonly IGenericService Service;

        public CreatePipeValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");

            RuleFor(x => x.MaterialQuantity).GreaterThan(0).When(x => x.ShowDetails).WithMessage("Equivalent lenght must be defined!");
            RuleFor(x => x.LaborQuantity).GreaterThan(0).When(x => x.ShowDetails).WithMessage("#Labor days must be defined!");

            RuleFor(x => x.LaborDayPrice).GreaterThan(0).When(x => x.ShowDetails).WithMessage("Unitary cost for Labor/day must be defined!");
            RuleFor(x => x.EquivalentLenghPrice).GreaterThan(0).When(x => x.ShowDetails).WithMessage("Unitary cost for equivalent lenght/mt must be defined!");


            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");

            RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Brand must be defined!");
            
            RuleFor(x => x.FluidCodeName).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Fluid must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.PipeClass).NotEqual(PipeClassEnum.None).When(x => x.ShowDetails)
          .WithMessage("Pipe Class must be defined!");


            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x => x.ShowDetails).WithMessage("Nozzles must be have on inlet and one outlet");
            RuleFor(x => x.TagNumber).NotEmpty().When(x => x.ShowDetails)
                .WithMessage("Tag Number must be defined!");

            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
                .MustAsync(ReviewIfTagExist)
               .WithMessage(x => $"{x.Tag} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType).When(x => x.ShowDetails)
              .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter).When(x => x.ShowDetails)
                .WithMessage("All diameter nozzle must be defined!");

        }

        async Task<bool> ReviewIfNameExist(CreatePipeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipeRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,



            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(CreatePipeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipeTagRequest validate = new()
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
    public class UpdatePipeValidator : AbstractValidator<UpdatePipeRequest>
    {
        private readonly IGenericService Service;

        public UpdatePipeValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");

            RuleFor(x => x.MaterialQuantity).GreaterThan(0).When(x => x.ShowDetails).WithMessage("Equivalent lenght must be defined!");
            RuleFor(x => x.LaborQuantity).GreaterThan(0).When(x => x.ShowDetails).WithMessage("#Labor days must be defined!");

            RuleFor(x => x.LaborDayPrice).GreaterThan(0).When(x => x.ShowDetails).WithMessage("Unitary cost for Labor/day must be defined!");
            RuleFor(x => x.EquivalentLenghPrice).GreaterThan(0).When(x => x.ShowDetails).WithMessage("Unitary cost for equivalent lenght/mt must be defined!");


            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");

            RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Brand must be defined!");

            RuleFor(x => x.FluidCodeName).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Fluid must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.PipeClass).NotEqual(PipeClassEnum.None).When(x => x.ShowDetails)
          .WithMessage("Pipe Class must be defined!");


            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x => x.ShowDetails).WithMessage("Nozzles must be have on inlet and one outlet");
            RuleFor(x => x.TagNumber).NotEmpty().When(x => x.ShowDetails)
                .WithMessage("Tag Number must be defined!");

            RuleFor(x => x.Tag).NotEmpty().When(x => x.ShowDetails)
                .MustAsync(ReviewIfTagExist)
               .WithMessage(x => $"{x.Tag} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

            RuleFor(x => x.Nozzles).Must(ReviewConnectionType).When(x => x.ShowDetails)
              .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter).When(x => x.ShowDetails)
                .WithMessage("All diameter nozzle must be defined!");

        }

        async Task<bool> ReviewIfNameExist(UpdatePipeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipeRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(UpdatePipeRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipeTagRequest validate = new()
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
