using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ecommerce.Classes
{
    public class FilesHelpers
    {
        public static bool UploadPhoto(HttpPostedFileBase file, string folder, string name)
        {
            if (file == null || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(name))
            {
                return false;
            }

            try
            {
                /*es la ruta completa*/
                string path = string.Empty;
                /*el nombre del archivo solo*/
                string pic = string.Empty;

                if (file != null)
                {
                    /*le asignamos el nombre de la ruta*/
                    pic = Path.GetFileName(file.FileName);
                    /*le asignamos la ruta del servidor*/
                    path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}