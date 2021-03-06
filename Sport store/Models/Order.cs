﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        
        [BindNever]
        public bool Shiped { get; set; }

        [Required(ErrorMessage ="Prosze podać imię i nazwisko.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Proszę podać pierwszy wiersz adresu.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage ="Prosze podać nazwę miasta")]
        public string City { get; set; }

        [Required(ErrorMessage ="Prosze podać nazwę województwo")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage ="Proszę podać nazwe kraju.")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
        
    }
}
