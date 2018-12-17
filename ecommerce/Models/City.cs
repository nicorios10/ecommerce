using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Display(Name = "City")]
        [Index("City_DepartmentId_Name_Index", 2, IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage ="Deve seleccionar un {0}")]
        [Index("City_DepartmentId_Name_Index", 1, IsUnique = true)]
        public int DepartmentId { get; set; }

        //del lado de muchos(un departamento tiene varias ciudades)
        //creamos un objeto del uno(ciudade)
        //virtual, no la manejamos en el modelo sino que va a virtualizarce
        public virtual Department Department { get; set; }

        //le decimos que una ciudad tiene varias companias
        public virtual IColection<Company> Company { get; set; }
        //le decimos que una ciudad tiene varios usuarios
        public virtual IColection<User> Users { get; set; }
    }
}