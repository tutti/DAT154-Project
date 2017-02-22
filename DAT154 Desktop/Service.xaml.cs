using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using DAT154_Libs;

namespace DAT154_Desktop {
    /// <summary>
    /// Interaction logic for Service.xaml
    /// </summary>
    public partial class Service : Page {

        ObservableCollection<DAT154_Libs.Task> tasks;

        public Service() {
            InitializeComponent();

            tasks = new ObservableCollection<DAT154_Libs.Task>();
            foreach (DAT154_Libs.Task task in FakeData.getTasks()) {
                tasks.Add(task);
            }
            taskList.ItemsSource = tasks;
        }

        private void addButtonClick(object sender, RoutedEventArgs e) {
            newPopup.IsOpen = true;
        }

        private void closeNew(object sender, MouseButtonEventArgs e) {
            newPopup.IsOpen = false;
        }
        private void newmouseoverX(object sender, MouseEventArgs e) {
            newpopupexitButton.Background = new SolidColorBrush() { Color = Colors.Red };
        }
        private void newmouseleaveX(object sender, MouseEventArgs e) {
            newpopupexitButton.Background = new SolidColorBrush() { Color = Colors.AliceBlue };
        }

        private void closeView(object sender, MouseButtonEventArgs e) {
            newPopup.IsOpen = false;
        }
        private void viewmouseoverX(object sender, MouseEventArgs e) {
            viewpopupexitButton.Background = new SolidColorBrush() { Color = Colors.Red };
        }
        private void viewmouseleaveX(object sender, MouseEventArgs e) {
            viewpopupexitButton.Background = new SolidColorBrush() { Color = Colors.AliceBlue };
        }
    }
}
