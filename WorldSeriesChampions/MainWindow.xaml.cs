using System.Windows;
using System.Windows.Controls;

namespace WorldSeriesChampions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
            vm.init();
        }

        private void TeamsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            vm.TeamSelected(lb.SelectedIndex);
        }
    }
}
