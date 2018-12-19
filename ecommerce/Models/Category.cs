using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Category")]
        [Index("Category_CompanyId_Description_Index", 1, IsUnique = true)]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Index("Category_CompanyId_Description_Index", 2, IsUnique = true)]
        [Display(Name= "Company")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual IColection<Product> Products { get; set; }

    }
}