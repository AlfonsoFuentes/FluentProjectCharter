using Shared.Enums.Materials;
using Shared.Models.Templates.Equipments.Requests;
using Shared.Models.Templates.Equipments.Validators;
using Shared.Models.Templates.NozzleTemplates;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.EquipmentTemplates
{
    public class CreateEquipmentTemplateValidator : AbstractValidator<CreateEquipmentTemplateRequest>
    {
        private readonly IGenericService Service;

        public CreateEquipmentTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Value must be defined!");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model must be defined!");
            RuleFor(x => x.ExternalMaterial).NotEqual(MaterialEnum.None).WithMessage("External Material must be defined!");
            RuleFor(x => x.InternalMaterial).NotEqual(MaterialEnum.None).WithMessage("Internal Material must be defined!");
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
        async Task<bool> ReviewIfNameExist(CreateEquipmentTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEquipmentTemplateRequest validate = new()
            {
                Name = name,
                Brand = request.BrandResponse!.Name,
                Model = request.Model,
                InternalMaterial = request.InternalMaterial.Name,
                ExternalMaterial = request.ExternalMaterial.Name,
                Type = request.Type,
                SubType = request.SubType,
                TagLetter = request.TagLetter,
                Reference = request.Reference



            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateEquipmentTemplateValidator : AbstractValidator<UpdateEquipmentTemplateRequest>
    {
        private readonly IGenericService Service;

        public UpdateEquipmentTemplateValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Value must be defined!");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model must be defined!");
            RuleFor(x => x.ExternalMaterial).NotEqual(MaterialEnum.None).WithMessage("External Material must be defined!");
            RuleFor(x => x.InternalMaterial).NotEqual(MaterialEnum.None).WithMessage("Internal Material must be defined!");
            RuleFor(x => x.Nozzles).Must(ReviewIfNozzleCount).WithMessage("Nozzle count must be greater than zero");
            RuleFor(x => x.BrandResponse).NotNull().WithMessage("Brand must be defined!");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x =>!string.IsNullOrEmpty(x.Brand))
                .WithMessage(x => $"Template already exist");

        }
        bool ReviewIfNozzleCount(List<NozzleTemplateResponse> nozzles)
        {
            return nozzles.Count > 0;
        }
        async Task<bool> ReviewIfNameExist(UpdateEquipmentTemplateRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEquipmentTemplateRequest validate = new()
            {
                Name = name,
                Brand = request.BrandResponse!.Name,
                Model = request.Model,
                InternalMaterial = request.InternalMaterial.Name,
                ExternalMaterial = request.ExternalMaterial.Name,
                Type = request.Type,
                SubType = request.SubType,
                TagLetter = request.TagLetter,
                Reference = request.Reference,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
