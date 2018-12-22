using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Descripcion")]
        [Index("State_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}