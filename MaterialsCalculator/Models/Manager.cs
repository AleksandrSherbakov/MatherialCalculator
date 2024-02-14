using System.Windows.Controls;

namespace MaterialsCalculator.Models
{
    public class Manager
    {
        /// <summary>
        /// Фрейм, в котором отбражаются Page
        /// </summary>
        public static Frame MainFrame { get; set; }
        /// <summary>
        /// Текущий пользователь системы
        /// </summary>
        public static User CurrentUser { get; set; }
    }
}
