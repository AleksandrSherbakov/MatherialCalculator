using System.Data.Entity;

namespace MaterialsCalculator.Models
{
    public partial class MaterialsCalculatorEntities: DbContext
    {
        private static MaterialsCalculatorEntities _context;
        /// <summary>
        /// Метод возвращающий контекст подключения
        /// </summary>
        /// <returns></returns>
        public static MaterialsCalculatorEntities GetContext()
        {
            if (_context == null)
            {
                _context = new MaterialsCalculatorEntities();
            }
            return _context;
        }
    }
}
