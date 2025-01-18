using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.Templates.Pipes.Requests;
using Shared.Models.Templates.Pipes.Validators;
using Shared.Models.Templates.NozzleTemplates;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Templates.PipeTemplates
{
    public class CreatePipeTemplateValidator : AbstractValidator<CreatePipeTemplateRequest>
    {
        private readonly IGenericService Service;

        public CreatePipeTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.LaborDayPrice).GreaterThan(0).WithMessage("Labor Day cost must be defined!");
            RuleFor(x => x.EquivalentLenghPrice).GreaterThan(0).WithMessage("Pipe lenght cost must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).WithMessage("Material must be defined!");
            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).WithMessage("Diameter must be defined!");
            RuleFor(x => x.Class).NotEqual(PipeClassEnum.None).WithMessage("Pipe class must be defined!");



            RuleFor(x => x.BrandResponse).NotNull().WithMessage("Brand must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
               .When(x => !string.IsNullOrEmpty(x.Brand))
               .WithMessage(x => $"Template already exist");

        }

        async Task<bool> ReviewIfNameExist(CreatePipeTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipeTemplateRequest validate = new()
            {
               
                Brand = request.Brand,
                Class = request.Class.Name,
                Diameter=request.Diameter.Name,
                Insulation=request.Insulation,



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
    public class UpdatePipeTemplateValidator : AbstractValidator<UpdatePipeTemplateRequest>
    {
        private readonly IGenericService Service;

        public UpdatePipeTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.LaborDayPrice).GreaterThan(0).WithMessage("Labor Day cost must be defined!");
            RuleFor(x => x.EquivalentLenghPrice).GreaterThan(0).WithMessage("Pipe lenght cost must be defined!");

            RuleFor(x => x.Material).NotEqual(MaterialEnum.None).WithMessage("Material must be defined!");
            RuleFor(x => x.Diameter).NotEqual(NominalDiameterEnum.None).WithMessage("Diameter must be defined!");
            RuleFor(x => x.Class).NotEqual(PipeClassEnum.None).WithMessage("Pipe class must be defined!");



            RuleFor(x => x.BrandResponse).NotNull().WithMessage("Brand must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Brand))
                .WithMessage(x => $"Template already exist");

        }

        async Task<bool> ReviewIfNameExist(UpdatePipeTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidatePipeTemplateRequest validate = new()
            {
                Brand = request.Brand,
                Class = request.Class.Name,
                Diameter = request.Diameter.Name,
                Insulation = request.Insulation,
                Id = request.Id

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
