using Shared.Enums.ConnectionTypes;
using Shared.Enums.DiameterEnum;
using Shared.Enums.Materials;
using Shared.Enums.NozzleTypes;
using Shared.Enums.ValvesEnum;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.BudgetItems.IndividualItems.Valves.Requests;
using Shared.Models.BudgetItems.IndividualItems.Valves.Responses;
using Shared.Models.BudgetItems.IndividualItems.Valves.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.BudgetItems.Valves
{
    public class ValveValidator : AbstractValidator<ValveResponse>
    {
        private readonly IGenericService Service;

        public ValveValidator(IGenericService service)
        {
            Service = service;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");
            RuleFor(x => x.BudgetUSD).GreaterThan(0).When(x => !x.IsExisting).WithMessage("Budget must be defined!");


            RuleFor(x => x.ProvisionalTag).NotEmpty().When(x => x.ShowProvisionalTag && !x.ShowDetails)
            .WithMessage("Tag must be defined!");

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

            RuleFor(x => x.BrandName).NotEmpty().When(x => x.ShowDetails)
             .WithMessage("Brand must be defined!");

            RuleFor(x => x.TagLetter).NotEmpty().When(x => x.ShowDetails)
              .WithMessage("Tag Letter must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewInletOutlet).When(x =>
         x.Type.Id != ValveTypesEnum.Ball_Three_Way_L.Id || x.Type.Id != ValveTypesEnum.Ball_Three_Way_T.Id || x.Type.Id != ValveTypesEnum.Ball_Four_Way.Id

         ).WithMessage("Nozzles must be have one inlet and one outlet");

            RuleFor(x => x.Nozzles).Must(ReviewThreeWayInletOutlet).When(x =>
           x.Type.Id == ValveTypesEnum.Ball_Three_Way_L.Id || x.Type.Id == ValveTypesEnum.Ball_Three_Way_T.Id || x.Type.Id == ValveTypesEnum.Ball_Four_Way.Id
           ).WithMessage("Nozzles must be have one inlet and one outlet and three nozzles");


            RuleFor(x => x.Nozzles).Must(ReviewFourWayInletOutlet).When(x =>
           x.Type.Id == ValveTypesEnum.Ball_Four_Way.Id
           ).WithMessage("Nozzles must be have one inlet and one outlet and four nozzles");


            RuleFor(x => x.Nozzles).Must(ReviewConnectionType).When(x => x.ShowDetails)
                .WithMessage("All connection types nozzle must be defined!");

            RuleFor(x => x.Nozzles).Must(ReviewDiameter).When(x => x.ShowDetails)
                .WithMessage("All diameter nozzle must be defined!");


            RuleFor(x => x.TagNumber).NotEmpty().When(x => x.ShowDetails)
                .WithMessage("Tag Number must be defined!");

            RuleFor(x => x.Tag)
                .MustAsync(ReviewIfTagExist)
                .When(x => x.ShowDetails && !x.ShowProvisionalTag)
               .WithMessage(x => $"{x.Tag} already exist");

            RuleFor(x => x.ProvisionalTag)
                .MustAsync(ReviewIfTagExist)
                .When(x => !x.ShowDetails && x.ShowProvisionalTag)
               .WithMessage(x => $"{x.ProvisionalTag} already exist");

            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(ValveResponse request, string name, CancellationToken cancellationToken)
        {
            ValidateValveRequest validate = new()
            {
                Name = name,
                Id = request.Id,
                ProjectId = request.ProjectId,



            };
            var result = await Service.Validate(validate);
            return !result;
        }
        async Task<bool> ReviewIfTagExist(ValveResponse request, string tag, CancellationToken cancellationToken)
        {
            ValidateValveTagRequest validate = new()
            {

                Tag = tag,
                ProjectId = request.ProjectId,
                Id = request.Id,

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
        bool ReviewThreeWayInletOutlet(List<NozzleResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Outlet.Id)) return false;
            if (nozzles.Count < 3) return false;

            return true;
        }
        bool ReviewFourWayInletOutlet(List<NozzleResponse> nozzles)
        {
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Inlet.Id)) return false;
            if (!nozzles.Any(x => x.NozzleType.Id == NozzleTypeEnum.Outlet.Id)) return false;
            if (nozzles.Count < 4) return false;

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
