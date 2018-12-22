
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ecommerce.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        /*El user name va as ser el mail*/
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(256, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "E-Mail")]
        [Index("User_UserName_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }
        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase PhotoFile { get; set; }


        /*Llave foranea*/
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        /*Llave foranea*/
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        /*Llave foranea*/
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        /*propiedades de lectura, para combobox*/
        [Display(Name = "User Full Name")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); }}

        //le decimos que una compania tiene varios departamentos
        public virtual Department Department { get; set; }
        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
    }
}