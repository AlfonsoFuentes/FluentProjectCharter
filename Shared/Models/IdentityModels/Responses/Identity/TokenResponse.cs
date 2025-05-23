﻿using System;

namespace Shared.Models.IdentityModels.Responses.Identity
{
    public class TokenResponse
    {
        public string UserId { get; set; }=string.Empty;    
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
      
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}