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

namespace DAT154_Desktop {
    /// <summary>
    /// Interaction logic for Service.xaml
    /// </summary>
    public partial class Service : Page {

        public Service() {
            InitializeComponent();
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
