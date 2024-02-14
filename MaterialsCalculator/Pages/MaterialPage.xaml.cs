using MaterialsCalculator.Models;
using MaterialsCalculator.Pages;
using MaterialsCalculator.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace MaterialsCalculator.Pages
{
	/// <summary>
	/// Логика взаимодействия для MaterialPage.xaml
	/// </summary>
	public partial class MaterialPage : Page
	{   
		public MaterialType materialType;
		public Material _curentMaterial;
		public MaterialPage(MaterialType materialType)
		{
			InitializeComponent();
			this.materialType = materialType;
			LoadAndInitData();           
		}
		void LoadAndInitData()
		{
			// загрузка мероприятий в ListBox сортируем по Дате
			var mce = MaterialsCalculatorEntities.GetContext().Materials.ToList().Where(p => p.MaterialTypeID==materialType.MaterialTypeID);
			ListBoxMaterials.ItemsSource = mce;
		}
		private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			//обновление данных после каждой активации окна
			if (Visibility == Visibility.Visible)
			{
				MaterialsCalculatorEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
			}
		}                                         
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.Tag is Material material)
			{
				ManufacturerPage page = new ManufacturerPage(material);
				Manager.MainFrame.Navigate(page);
			}			
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
				if (Convert.ToInt32(TBoxSquare.Text) / Convert.ToInt32(_curentMaterial.Square) <= Convert.ToInt32(_curentMaterial.Count))
					Result.Text = (Convert.ToInt32(TBoxSquare.Text) / Convert.ToInt32(_curentMaterial.Square) * Convert.ToInt32(_curentMaterial.Price)).ToString() + " рублей";
			}
			catch (Exception ex)
            {
				MessageBox.Show("Выберите метериал");
            }
			
		}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
			if (sender is Button button && button.Tag is Material material)
			{
				_curentMaterial = material;
			}
		}
    }
}
