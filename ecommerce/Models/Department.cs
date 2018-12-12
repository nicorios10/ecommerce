using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage ="El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "Department")]
        //para no tener nombres repetidos
        //desde db estamos controlando que no halla 2 departamentos
        //con el mismo nombre
        [Index("Department_Name_Index", IsUnique =true)]
        public string Name { get; set; }

        //le decimos que un depatamento tiene varias ciudades
        public virtual IColection<City> Cities { get; set; }
        //le decimos que un depatamento tiene varias companias
        public virtual IColection<Company> Companies { get; set; }
        //le decimos que un depatamento tiene varios usuarios
        public virtual IColection<User> Users { get; set; }
    }
}