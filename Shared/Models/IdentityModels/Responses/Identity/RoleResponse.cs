﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Models.IdentityModels.Responses.Identity
{
    public class RoleResponse
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}