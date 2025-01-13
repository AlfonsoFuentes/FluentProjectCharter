using Shared.Enums.Materials;
using Shared.Models.BudgetItems.Equipments.Requests;
using Shared.Models.BudgetItems.Equipments.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.Equipments
{
    public class CreateEquipmentValidator : AbstractValidator<CreateEquipmentRequest>
    {
        private readonly IGenericService Service;

        public CreateEquipmentValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Type).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Type must be defined!");

            RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Brand must be defined!");

            RuleFor(x => x.InternalMaterial).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.ExternalMaterial).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.Model).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Model must be defined!");

            RuleFor(x => x.Reference).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Reference must be defined!");

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

        async Task<bool> ReviewIfNameExist(CreateEquipmentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEquipmentRequest validate = new()
            {
                Name = name,

                DeliverableId = request.DeliverableId,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(CreateEquipmentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEquipmentTagRequest validate = new()
            {
                Name = name,

                Tag = request.Tag,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
    public class UpdateEquipmentValidator : AbstractValidator<UpdateEquipmentRequest>
    {
        private readonly IGenericService Service;

        public UpdateEquipmentValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.Budget).GreaterThan(0).WithMessage("Budget must be defined!");
            RuleFor(x => x.TagLetter).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");
            RuleFor(x => x.Type).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Type must be defined!");

            RuleFor(x => x.Brand).NotEmpty().When(x => x.ShowDetails)
           .WithMessage("Brand must be defined!");

            RuleFor(x => x.InternalMaterial).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.ExternalMaterial).NotEqual(MaterialEnum.None).When(x => x.ShowDetails)
          .WithMessage("Internal Material must be defined!");

            RuleFor(x => x.Model).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Model must be defined!");

            RuleFor(x => x.Reference).NotEmpty().When(x => x.ShowDetails)
          .WithMessage("Reference must be defined!");

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

        async Task<bool> ReviewIfNameExist(UpdateEquipmentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEquipmentRequest validate = new()
            {
                Name = name,
                DeliverableId = request.DeliverableId,
                Id = request.Id,
                ProjectId = request.ProjectId,

            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(UpdateEquipmentRequest request, string name, CancellationToken cancellationToken)
        {
            ValidateEquipmentTagRequest validate = new()
            {
                Name = name,
                Id = request.Id,
                Tag = request.Tag,
                ProjectId = request.ProjectId,


            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
