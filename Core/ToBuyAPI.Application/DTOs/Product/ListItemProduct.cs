﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.Application.DTOs.Product
{
    public class ListItemProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ListItemCategory> Categories { get; set; }
    }
}
