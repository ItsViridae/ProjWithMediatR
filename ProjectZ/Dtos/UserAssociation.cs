﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectZ.Dtos
{
    public class GetUserAssociationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AssociationId { get; set; }
    }
}
