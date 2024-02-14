using System;
using System.IO;

namespace MaterialsCalculator.Models
{
    public partial class MaterialType
    {
        public string GetPicture
        {
            get
            {
                if (Picture is null)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return Directory.GetCurrentDirectory() + @"\Images\MaterialTypes\" + Picture.Trim();
            }
        }
    }
}
