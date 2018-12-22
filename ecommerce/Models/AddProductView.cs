using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class AddProductView
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Deve ingresar un numero mayor que {1}.")]
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }
    }
}