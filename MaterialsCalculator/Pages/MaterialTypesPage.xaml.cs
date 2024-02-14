using MaterialsCalculator.Models;
using MaterialsCalculator.Pages;
using MaterialsCalculator.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaterialsCalculator.Pages
{
	/// <summary>
	/// Логика взаимодействия для MaterialTypesPage.xaml
	/// </summary>
	public partial class MaterialTypesPage : Page
	{    

		private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			//обновление данных после каждой активации окна
			if (Visibility == Visibility.Visible)
			{
				MaterialsCalculatorEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
			}
		}

		private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateData();
		}

		int _itemcount = 0; // количество записей в таблице Events
		public MaterialTypesPage()
		{
			InitializeComponent();
			LoadAndInitData();
		}
		/// <summary>
		/// Загрузка и подготовка данных страницы
		/// </summary>
		void LoadAndInitData()
		{
			// загрузка мероприятий в ListBox сортируем по Дате
			var mce = MaterialsCalculatorEntities.GetContext().MaterialTypes.ToList();
			ListBoxMaterialTypes.ItemsSource = mce;
			// запоминаем общще количество мероприятий в БД
			_itemcount = ListBoxMaterialTypes.Items.Count;

			TextBlockInfo.Text = $" Результат запроса: {_itemcount} записей из {_itemcount}";
		}

		/// <summary>
		/// Метод для фильтрации и сортировки данных
		/// </summary>
		private void UpdateData()
		{
			// получаем актуальный список мероприятий из БД
			var currentEvents = MaterialsCalculatorEntities.GetContext().MaterialTypes.OrderBy(p => p.MaterialTypeID).ToList();

			// выбор тех мероприятий, в названии которых есть поисковая строка
			currentEvents = currentEvents.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

			// В качестве источника данных присваиваем список данных
			ListBoxMaterialTypes.ItemsSource = currentEvents;
			// отображение количества записей
			TextBlockInfo.Text = $" Результат запроса: {currentEvents.Count} " +
			$"записей из {_itemcount}";
		}

		private void MaterialTypeButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.Tag is MaterialType materialType)
			{
				// Открываете новую страницу с выбранным типом материала
				OpenNewPageWithMaterialType(materialType);
				
			}
		}

		private void OpenNewPageWithMaterialType(MaterialType materialType)
		{
			// Создаете новую страницу с выбранным типом материала
			MaterialPage detailsPage = new MaterialPage(materialType);

			// Навигируете к новой странице
			if (Manager.MainFrame != null)
			{
				Manager.MainFrame.Navigate(detailsPage);
			}
		}
	}
}
