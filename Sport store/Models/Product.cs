using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Sport_store.Models
{

    public class Product
    {
      
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę produktu")]
        [Display(Name="Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać opis.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią liczbę.")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

       
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Proszę określić kategorię.")]
        [Display(Name = "Kategoria")]
        public Category Category { get; set; }



    }
}
