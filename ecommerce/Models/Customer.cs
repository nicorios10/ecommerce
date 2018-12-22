using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        /*El user name va as ser el mail*/
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(256, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "E-Mail")]
        [Index("Customer_UserName_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Primer Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }

        [Display(Name = "Comprador Nombre Completo")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        
        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}