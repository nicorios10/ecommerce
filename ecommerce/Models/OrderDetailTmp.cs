using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class OrderDetailTmp
    {
        [Key]
        public int OrderDetailTmpId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(256, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "E-Mail")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Descripcion del Producto")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "Deve seleccionar un {0} de entre {1} y {2}")]
        [Display(Name = "Tax Rate")]
        public double TaxRate { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Deve seleccionar un {0} de entre {1} y {2}")]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Deve seleccionar un {0} de entre {1} y {2}")]
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }

        //propiedad de lectura
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        public decimal Value { get { return Price * (decimal)Quantity; } }

        public virtual Product Product { get; set; }
    }
}