using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ITCompany.Models
{
    public class ImagesRepo
    {
        private static string imageSource;

        public byte[] ClientsGet()
        {
            imageSource = ConfigurationManager.AppSettings["ClientsImageSource"];
            return File.ReadAllBytes(imageSource);
        }

        public byte[] EmployeesGet()
        {
            imageSource = ConfigurationManager.AppSettings["EmployeesImageSource"];
            return File.ReadAllBytes(imageSource);
        }

        public byte[] ProjectsGet()
        {
            imageSource = ConfigurationManager.AppSettings["ProjectsImageSource"];
            return File.ReadAllBytes(imageSource);
        }

        public void Update(byte[] content)
        {
            File.WriteAllBytes(imageSource, content);
        }
    }
}