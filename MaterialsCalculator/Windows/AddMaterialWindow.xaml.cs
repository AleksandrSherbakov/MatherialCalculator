using MaterialsCalculator.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MaterialsCalculator.Pages;
using MaterialsCalculator.Windows;
using System.Windows.Media.Imaging;

namespace MaterialsCalculator.Windows
{
	/// <summary>
	/// Логика взаимодействия для AddMaterialWindow.xaml
	/// </summary>
	public partial class AddMaterialWindow : Window
	{
		private Material _currentMaterial = new Material();

		// путь к загружаемой фтографии
		private string _filePath = null;
		// имя файла
		private string _photoName = null;
		// путь к папке с фотографиями модераторов
		public AddMaterialWindow()
		{
			InitializeComponent();
			this.DataContext = _currentMaterial;
		}
		/*void LoadAndInitData()
		{
			// подгружаем данные выпадающих списков
			// страны
			ComboBoxType.ItemsSource = MaterialsCalculatorEntities.GetContext().MaterialTypes.ToList();

		}*/
		/// <summary>
		/// Проверка полей ввод на корректыне данные
		/// </summary>
		/// <returns>текст StringBuilder содержащий ошибки, если они есть</returns>
	
		private void BtnOk_Click(object sender, RoutedEventArgs e)
		{
			using (var transaction =
			MaterialsCalculatorEntities.GetContext().Database.BeginTransaction())
			{
				try
				{
					_currentMaterial.Name = TBName.Text;
					_currentMaterial.Description = TBDescription.Text;
					//_currentMaterial.Image = TextBoxMiddleName.Text;
					_currentMaterial.MaterialTypeID = ComboBoxType.SelectedIndex+1;
					_currentMaterial.ManufacturerID = ComboBoxType.SelectedIndex+1;
					_currentMaterial.Square = TBSquare.Text;
					_currentMaterial.Price = TBPrice.Text;
					_currentMaterial.Count = TBCount.Text;

					string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\Materials\";
					string photo = ChangePhotoName(_currentDirectory);
					string dest = _currentDirectory + photo;
					File.Copy(_filePath, dest);
					_currentMaterial.Image = photo;

					MaterialsCalculatorEntities.GetContext().Materials.Add(_currentMaterial);
					MaterialsCalculatorEntities.GetContext().SaveChanges();

					transaction.Commit();
					// Сохраняем изменения в БД
					MessageBox.Show("Запись добавлена");
					DialogResult = true; // Возвращаемся на предыдущую форму
				}
				catch (Exception ex)
				{
					transaction.Rollback();                                      
					MessageBox.Show(ex.Message.ToString());
				}
			}
		}

		//подбор имени файла
		string ChangePhotoName(string _currentDirectory)
		{
			string x = _currentDirectory + _photoName;
			string photoname = _photoName;

			int i = 0;
			if (File.Exists(x))
			{
				while (File.Exists(x))
				{
					i++;
					x = _currentDirectory + i.ToString() + photoname;
				}
				photoname = i.ToString() + photoname;
			}
			return photoname;
		}
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void ButtonLoadPhoto_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//Диалог открытия файла
				OpenFileDialog op = new OpenFileDialog
				{
					Title = "Select a picture",
					Filter = "PNG Files (*.png)|*.png"
				};
				// диалог вернет true, если файл был открыт
				if (op.ShowDialog() == true)
				{
					ImageMaterialPhoto.Source = new BitmapImage(new Uri(op.FileName));
					_photoName = op.SafeFileName;
					_filePath = op.FileName;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
				MessageBoxImage.Error);
				_filePath = null;
			}
		}
	}
}
