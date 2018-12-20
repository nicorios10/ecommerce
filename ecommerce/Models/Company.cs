using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Company")]
        [Index("Company_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [NotMapped]//para que no se guarde en db, solo lo queremos de forma temporal
                   //para manipular la imagen
                   /*la propiedad Logo es la ruto y PhotoFile es el archivo*/
        [Display(Name = "Imagen")]
        public HttpPostedFileBase LogoFile { get; set; }

        /*Llave foranea*/
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        public int DepartmentId { get; set; }

        /*Llave foranea*/
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        public int CityId { get; set; }

        


        //le decimos que una compania tiene varios departamentos
        public virtual Department Department { get; set; }
        //le decimos que una compania tiene varias ciudades
        public virtual City City { get; set; }


        //le decimos que una compania tiene varios usuarios
        public virtual ICollection<User> Users { get; set; }       
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }

    }
}