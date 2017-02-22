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
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Page {
        public Booking() {
            InitializeComponent();
        }

        private void closeBooking(object sender, MouseButtonEventArgs e) {
            newbookingPopup.IsOpen = false;
        }

        private void mouseoverX(object sender, MouseEventArgs e) {
            popupexitButton.Background = new SolidColorBrush() { Color = Colors.Red };
        }

        private void mouseleaveX(object sender, MouseEventArgs e) {
            popupexitButton.Background = new SolidColorBrush() { Color = Colors.AliceBlue };
        }
    }
}
