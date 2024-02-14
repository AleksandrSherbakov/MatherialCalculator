using MaterialsCalculator.Models;
using MaterialsCalculator.Pages;
using MaterialsCalculator.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaterialsCalculator.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManufacturerPage.xaml
    /// </summary>
    public partial class ManufacturerPage : Page
    {
        public Material material { get; }

        public ManufacturerPage(Material material)
        {
            InitializeComponent();
            this.material = material;
            this.LoadData();

        }
        public void LoadData()
        {
            try
            {
                Manufacturer manufacturer = MaterialsCalculatorEntities.GetContext().Manufacturers.ToList().Where(p => p.ManufacturerID == material.ManufacturerID).FirstOrDefault();
                TBName.Text = $"Manufacturer Name: {manufacturer.Name}";
                TBDeccription.Text = $"Description: {manufacturer.Description}";
                TBEmail.Text = $"Email: {manufacturer.Email}";
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Похоже, что-то пошло не так :-(" + ex.Message);
                TBName.Text = $"Manufacturer Name: 404";
                TBDeccription.Text = $"Description: 404";
                TBEmail.Text = $"Email: 404";
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //обновление данных после каждой активации окна
            if (Visibility == Visibility.Visible)
            {
                MaterialsCalculatorEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            }
        }
    }
}
