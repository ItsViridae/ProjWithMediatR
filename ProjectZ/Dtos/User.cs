﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vinformatix.Security;

namespace ProjectZ.Dtos
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }


    public class GetUserAuthenticationDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}
