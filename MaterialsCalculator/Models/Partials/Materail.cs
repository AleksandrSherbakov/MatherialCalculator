using System;
using System.IO;

namespace MaterialsCalculator.Models
{
    public partial class Material
    {
        public string GetImage
        {
            get
            {
                if (Image is null)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return Directory.GetCurrentDirectory() + @"\Images\Materials\" + Image.Trim();
            }
        }
    }
}
