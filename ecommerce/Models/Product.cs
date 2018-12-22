using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Index("Product_CompanyId_Description_Index", 1, IsUnique = true)]
        [Index("Product_CompanyId_BarCode_Index", 1, IsUnique = true)]
        [Display(Name = "Compania")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Index("Product_CompanyId_Description_Index", 2, IsUnique = true)]
        [Display(Name = "Nombre")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(13, ErrorMessage = "El campo {0} deve contener menos de {1} caracteres")]
        [Index("Product_CompanyId_BarCode_Index", 2, IsUnique = true)]
        [Display(Name = "Codigo de Barra")]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Deve seleccionar un {0}")]
        [Display(Name = "Impuesto")]
        public int TaxId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Deve seleccionar un {0} de entre {1} y {2}")]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name ="Imagen")]
        public HttpPostedFileBase ImageFile { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string Remarks { get; set; }

        //propiedad de solo lectura
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get { return Inventories.Sum(i => i.Stock); } }

        //FK's
        public virtual Company Company { get; set; }
        public virtual Category Category { get; set; }
        public virtual Tax Tax { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderDetailTmp> OrderDetailTmps { get; set; }

    }
}