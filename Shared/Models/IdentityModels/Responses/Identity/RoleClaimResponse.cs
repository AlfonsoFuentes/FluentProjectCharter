﻿namespace Shared.Models.IdentityModels.Responses.Identity
{
    public class RoleClaimResponse
    {
        public int Id { get; set; }
        public string? RoleId { get; set; }
        public string? Type { get; set; } = string.Empty;
        public string? Value { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Group { get; set; } = string.Empty;
        public bool Selected { get; set; }
    }
}