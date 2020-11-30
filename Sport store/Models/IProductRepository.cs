﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Models
{
     public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
