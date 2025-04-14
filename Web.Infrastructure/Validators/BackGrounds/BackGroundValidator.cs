﻿using Shared.Models.Backgrounds.Requests;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Backgrounds.Validators;
using Web.Infrastructure.Managers.Generic;

namespace Web.Infrastructure.Validators.BackGrounds
{

    public class BackGroundValidator : AbstractValidator<BackGroundResponse>
    {
        private readonly IGenericService Service;

        public BackGroundValidator(IGenericService service)
        {
            Service = service;
                  RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be defined!");


            RuleFor(x => x.Name).MustAsync(ReviewIfNameExist)
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage(x => $"{x.Name} already exist");

        }

        async Task<bool> ReviewIfNameExist(BackGroundResponse request, string name, CancellationToken cancellationToken)
        {
            ValidateBackGroundRequest validate = new()
            {
                Name = name,
                ProjectId = request.ProjectId,
                Id = request.Id

            };
            var result = await Service.Validate(validate);
            return !result;
        }
    }
}
