using FluentValidation;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.StakeHolders.Requests;

namespace Web.Infrastructure.Validators.BackGrounds
{
    public class CreateStakeHolderInsideProjectValidator : AbstractValidator<CreateStakeHolderInsideProjectRequest>
    {
       

        public CreateStakeHolderInsideProjectValidator()
        {
     
            RuleFor(x => x.StakeHolder).NotNull().WithMessage("Name must be defined!");
           
            RuleFor(x => x.Role).Must(ReviewRole).WithMessage("Role must be defined!");
     

        }

        
        bool ReviewRole(CreateStakeHolderInsideProjectRequest request, StakeHolderRoleEnum roleEnum)
        {
            return roleEnum != StakeHolderRoleEnum.None;
        }
    }
    public class UpdateStakeHolderInsideProjectValidator : AbstractValidator<UpdateStakeHolderInsideProjectRequest>
    {


        public UpdateStakeHolderInsideProjectValidator()
        {

            RuleFor(x => x.StakeHolder).NotNull().WithMessage("Name must be defined!");

            RuleFor(x => x.Role).Must(ReviewRole).WithMessage("Role must be defined!");


        }


        bool ReviewRole(UpdateStakeHolderInsideProjectRequest request, StakeHolderRoleEnum roleEnum)
        {
            return roleEnum != StakeHolderRoleEnum.None;
        }
    }
}
