using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LandData.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //проверка без визуала
            var importer = new Services.ExcelImportService();
            importer.ImportFarmHouse(@"D:\Курсовая 2026\LandData\Таблицы\Жилой дом КФХ на сельхозке..xlsx");
            importer.ImportRedistribution(@"D:\Курсовая 2026\LandData\Таблицы\Таблица стоимость перераспределения участка.xlsx");

            var first = importer.Regions[0];
            MessageBox.Show($"Регионов: {importer.Regions.Count}\n" +
                            $"{first.Name}: правил {first.Rules.Count}\n" +
                            $"Категории: {string.Join(", ", first.Rules.Select(r => r.Category))}");
        }
    }
}