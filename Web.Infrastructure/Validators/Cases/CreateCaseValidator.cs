﻿namespace Web.Infrastructure.Validators.Cases
{
    //public class CreateCaseValidator : AbstractValidator<CreateCaseRequest>
    //{
    //    private readonly IGenericService Service;

    //    public CreateCaseValidator(IGenericService service)
    //    {
    //        Service = service;
    //        RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

    //        //RuleFor(x => x.OrganizationStrategy).NotNull().WithMessage("Must follow a strategy in the organization");

    //        RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
    //            .When(x => !string.IsNullOrEmpty(x.Name))
    //            .WithMessage(x => $"{x.Name} already exist");

    //    }

    //    async Task<bool> ReviewIfNameExist(CreateCaseRequest request, string name, CancellationToken cancellationToken)
    //    {
    //        ValidateCaseRequest validate = new()
    //        {
    //            Name = name,
    //            ProjectId = request.ProjectId,

    //        };
    //        var result = await Service.Validate(validate);
    //        return !result;
    //    }
    //}
    //public class UpdateCaseValidator : AbstractValidator<UpdateCaseRequest>
    //{
    //    private readonly IGenericService Service;

    //    public UpdateCaseValidator(IGenericService service)
    //    {
    //        Service = service;
    //        RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");

    //        //RuleFor(x => x.OrganizationStrategy).NotNull().WithMessage("Must follow a strategy in the organization");

    //        RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
    //            .When(x => !string.IsNullOrEmpty(x.Name))
    //            .WithMessage(x => $"{x.Name} already exist");

    //    }

    //    async Task<bool> ReviewIfNameExist(UpdateCaseRequest request, string name, CancellationToken cancellationToken)
    //    {
    //        ValidateCaseRequest validate = new()
    //        {
    //            Name = name,
    //            ProjectId = request.ProjectId,
    //            Id = request.Id

    //        };
    //        var result = await Service.Validate(validate);
    //        return !result;
    //    }
    //}
}
