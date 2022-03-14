﻿using fakeLook_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth_example.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user);
        public string GetPayload(string token);
    }
}
