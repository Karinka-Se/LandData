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
            //var importer = new Services.ExcelImportService();


            //importer.ImportFarmHouse(@"D:\Курсовая 2026\LandData\Таблицы\Жилой дом КФХ на сельхозке..xlsx");
            //importer.ImportRedistribution(@"D:\Курсовая 2026\LandData\Таблицы\Таблица стоимость перераспределения участка.xlsx");
            //importer.ImportBuyoutAfter3Years(@"D:\Курсовая 2026\LandData\Таблицы\Цена выкупа сельхоз участка через 3 года..xlsx");
            //importer.ImportAllRegionsPrices(@"D:\Курсовая 2026\LandData\Таблицы\Цены на все регионы.xlsx"); 

            //var bash = importer.Regions.First(r => r.Code == "02");
            //MessageBox.Show($"Регионов: {importer.Regions.Count}\n" +
            //    string.Join("\n", bash.Rules.Select(r => $"{r.Category}: {r.ValueText}")));
        }
    }
}